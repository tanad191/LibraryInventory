using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LibraryInventoryTracker.Models;

public class BookArchiveViewModel
{
    public List<Book>? Books { get; set; }
    public SelectList? Authors { get; set; }
    public string? BookAuthor { get; set; }
    public string? SearchString { get; set; }
}