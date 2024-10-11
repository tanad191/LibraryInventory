using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryInventoryTracker.Data;
using LibraryInventoryTracker.Models;
using System.Security.Policy;

namespace LibraryInventoryTracker.Controllers
{
    public class UserController : Controller
    {
        private readonly LibraryInventoryTrackerContext _context;
        private List<string> Usernames;

        public UserController(LibraryInventoryTrackerContext context)
        {
            _context = context;
            Usernames = (from s in _context.User
                select s.UserName).ToList<string>();
        }

        // GET: User
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("LoggedOn") == "True" && HttpContext.Session.GetString("Category") == "LIBRARIAN") {
                return View(await _context.User.ToListAsync());
            } else {
                return RedirectToAction("Error", "Home"); //redir to list
            }
        }

        // GET: User/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            List<SelectListItem> categoryList = new List<SelectListItem>();
            categoryList.Add(new SelectListItem{Value="0",Text="Customer"});
            categoryList.Add(new SelectListItem{Value="1",Text="Librarian"});

            ViewData.Add("Categories", categoryList);
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,UserName,Password,Category,IsActive")] User user)
        {
            if (ModelState.IsValid)
            {
                if (Usernames.Contains(user.UserName)) { //Ensures that duplicate ISBNs are not allowed
                        ViewBag.ErrorMessage = string.Format("ERROR: An account already exists with the username {0}.",nameof(user.UserName));
                }
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,UserName,Password,Category,IsActive")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }
            var prevUser = from s in _context.User.AsNoTracking()
                            where s.UserID == id
                            select s;

            //Create a list of all existing ISBNs EXCEPT the one for the book being edited
            List<string> UsernamesEdited = Usernames;
            UsernamesEdited.Remove(prevUser.First().UserName);

            if (ModelState.IsValid)
            {
                try
                {
                    if (UsernamesEdited.Contains(user.UserName)) { //Ensures that duplicate ISBNs are not allowed
                        ViewBag.ErrorMessage = string.Format("ERROR IN EDITING ACCOUNT {0}: An account with this username already exists.",nameof(user.UserID));
                        return View(user);
                    } else {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserID == id);
        }
    }
}
