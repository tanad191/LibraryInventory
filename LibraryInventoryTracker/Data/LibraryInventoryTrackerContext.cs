using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibraryInventoryTracker.Models;

namespace LibraryInventoryTracker.Data
{
    public class LibraryInventoryTrackerContext : DbContext
    {
        public LibraryInventoryTrackerContext (DbContextOptions<LibraryInventoryTrackerContext> options)
            : base(options)
        {
        }

        public DbSet<LibraryInventoryTracker.Models.Book> Book { get; set; } = default!;
    }
}
