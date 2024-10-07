using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryInventoryTracker.Models
{
    public class Book
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string CoverImage { get; set; }
        
        public string Publisher { get; set; }

        public string PublicationDate { get; set; }

        public string Category { get; set; }

        public string ISBN { get; set; }
        
        public int PageCount { get; set; }
        
        public bool CheckedOut { get; set; }

        public int? CheckoutID { get; set; }
    }
}