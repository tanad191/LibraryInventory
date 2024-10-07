using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryInventoryTracker.Models
{
    public enum Category
    {
        CUSTOMER = 1, LIBRARIAN = 2
    }
    public class User
    {
        public int UserID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public Category Category { get; set; }
        public Boolean IsActive { get; set; }
    }
}