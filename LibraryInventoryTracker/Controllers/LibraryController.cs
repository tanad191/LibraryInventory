using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryInventoryTracker.Data;
using LibraryInventoryTracker.Models;

namespace LibraryInventoryTracker.Controllers
{
    public class LibraryController : Controller
    {
        private readonly LibraryInventoryTrackerContext _context;

        public LibraryController(LibraryInventoryTrackerContext context)
        {
            _context = context;
        }
        
        // GET: Books
        public async Task<IActionResult> Archive(string? BookAuthor, string? searchString)
        {
            if (string.IsNullOrEmpty(searchString)) {
                return View("Archive");
            }

            if (_context.Book == null)
            {
                return Problem("Entity set 'LibraryInventoryTrackerContext.Book'  is null.");
            }

            // Use LINQ to get list of Authors.
            IQueryable<string> AuthorQuery = from m in _context.Book
                                            orderby m.Author
                                            select m.Author;
            var Books = from m in _context.Book
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                Books = Books.Where(s => s.Title!.ToUpper().Contains(searchString.ToUpper()));
            }

            if (!string.IsNullOrEmpty(BookAuthor))
            {
                Books = Books.Where(x => x.Author == BookAuthor);
            }

            var BookAuthorVM = new BookArchiveViewModel
            {
                Authors = new SelectList(await AuthorQuery.Distinct().ToListAsync()),
                Books = await Books.ToListAsync()
            };

            return View("Archive", BookAuthorVM);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Book == null)
            {
                return NotFound();
            }

            return View("Details", Book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View("Create");
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Description,CoverImage,Publisher,PublicationDate,Category,ISBN,PageCount")] Book Book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Archive));
            }
            return View("Create", Book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Book = await _context.Book.FindAsync(id);
            if (Book == null)
            {
                return NotFound();
            }
            return View("Edit", Book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Description,CoverImage,Publisher,PublicationDate,Category,ISBN,PageCount")] Book Book)
        {
            if (id != Book.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(Book.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Archive));
            }
            return View("Edit", Book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Book = await _context.Book
                .FirstOrDefaultAsync(m => m.ID == id);
            if (Book == null)
            {
                return NotFound();
            }

            return View("Delete", Book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Book = await _context.Book.FindAsync(id);
            if (Book != null)
            {
                _context.Book.Remove(Book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Archive));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.ID == id);
        }
    }
}