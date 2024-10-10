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

namespace LibraryInventoryTracker.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryInventoryTrackerContext _context;

        public BookController(LibraryInventoryTrackerContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            //Set up search tags
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "Title" : "";
            ViewBag.AuthorSortParm = String.IsNullOrEmpty(sortOrder) ? "Author" : "";
            ViewBag.AvailableSortParm = String.IsNullOrEmpty(sortOrder) ? "Availability" : "";
            
            //Get full book data from context and store in a dummy variable
            var booksFull = from s in _context.Book
                            select s;
            var books = booksFull; //Full database and data to present are stored separately in case we need to restore the full data

            //Replace the value contained in the dummy variable with the filtered data and present the dummy variable - if no filters, the dummy variable has the same value as the full data
            
            //Sort by column
            switch (sortOrder) {
                case "Title":
                    books = books.OrderBy(s => s.Title);
                    break;
                case "Author":
                    books = books.OrderBy(s => s.Author);
                    break;
                case "Availability":
                    books = books.Where(s => s.CheckedOut == false);
                    break;
                default:
                    books = books.OrderBy(s => s.ID);
                    break;
            }

            //Search by title
            if (!String.IsNullOrEmpty(searchString)) {
                books = books.Where(s => s.Title.Contains(searchString));
            }

            return View(await books.ToListAsync());
        }
        
        // GET: Book
        public async Task<IActionResult> Featured()
        {
            return PartialView(await _context.Book.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Author,Description,CoverImage,Publisher,PublicationDate,Category,ISBN,PageCount,CheckedOut")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Author,Description,CoverImage,Publisher,PublicationDate,Category,ISBN,PageCount,CheckedOut")] Book book)
        {
            if (id != book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
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
            return View(book);
        }
        
        // GET: Book/Checkout/5
        public async Task<IActionResult> Checkout(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Checkout/5
        [HttpPost, ActionName("Checkout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckoutConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            int? checkedoutID = Int32.TryParse(HttpContext.Session.GetString("UserID"), out var tempVal) ? tempVal : null;
            if (book != null)
            {
                book.CheckedOut = true;
                book.CheckoutID = checkedoutID;
                book.CheckoutDate = DateTime.Now;
                book.DueDate = DateTime.Now.AddDays(5);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Book/Return/5
        public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Return/5
        [HttpPost, ActionName("Return")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                book.CheckedOut = false;
                book.CheckoutID = null;
                book.CheckoutDate = null;
                book.DueDate = null;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
        
    }
}
