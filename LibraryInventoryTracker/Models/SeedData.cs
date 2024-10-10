using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryInventoryTracker.Data;
using System;
using System.Linq;

namespace LibraryInventoryTracker.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new LibraryInventoryTrackerContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<LibraryInventoryTrackerContext>>()))
        {
            // Look for any books.
            if (context.Book.Any())
            {
                return;   // DB has been seeded
            }
            context.Book.AddRange(
                new Book
                {
                    ID = 1,
                    Title = "Dracula",
                    Author = "Bram Stoker",
                    Description = "Dracula is a 1897 gothic horror novel by Irish author Bram Stoker. An epistolary novel, the narrative is related through letters, diary entries, and newspaper articles. It has no single protagonist and opens with solicitor Jonathan Harker taking a business trip to stay at the castle of a Transylvanian nobleman, Count Dracula. Harker escapes the castle after discovering that Dracula is a vampire, and the Count moves to England and plagues the seaside town of Whitby. A small group, led by Abraham Van Helsing, investigate, hunt and kill Dracula.",
                    CoverImage = "https://upload.wikimedia.org/wikipedia/commons/4/45/Dracula_1st_ed_cover_reproduction.jpg",
                    Publisher = "Archibald Constable and Company",
                    PublicationDate = "26 May 1897",
                    Category = "Horror",
                    ISBN = "978-1503261389",
                    PageCount = 418,
                    CheckedOut = false
                },
                new Book
                {
                    ID = 2,
                    Title = "The Hobbit, or There and Back Again",
                    Author = "J.R.R. Tolkien",
                    Description = "The Hobbit, or There and Back Again is a children's fantasy novel by the English author J. R. R. Tolkien. It was published in 1937 to wide critical acclaim, being nominated for the Carnegie Medal and awarded a prize from the New York Herald Tribune for best juvenile fiction. The Hobbit is set in Middle-earth and follows home-loving Bilbo Baggins, the hobbit of the title, who joins the wizard Gandalf and the thirteen dwarves of Thorin's Company, on a quest to reclaim the dwarves' home and treasure from the dragon Smaug. Bilbo's journey takes him from his peaceful rural surroundings into more sinister territory.",
                    CoverImage = "https://upload.wikimedia.org/wikipedia/en/4/4a/TheHobbit_FirstEdition.jpg",
                    Publisher = "George Allen & Unwin",
                    PublicationDate = "21 September 1937",
                    Category = "Fantasy",
                    ISBN = "978-0547928227",
                    PageCount = 310,
                    CheckedOut = false
                },
                new Book
                {
                    ID = 3,
                    Title = "Gone with the Wind",
                    Author = "Margaret Mitchell",
                    Description = "Gone with the Wind is a novel by American writer Margaret Mitchell, first published in 1936. The story is set in Clayton County and Atlanta, both in Georgia, during the American Civil War and Reconstruction Era. It depicts the struggles of young Scarlett O'Hara, the spoiled daughter of a well-to-do plantation owner, who must use every means at her disposal to claw her way out of poverty following Sherman's destructive \"March to the Sea.\" This historical novel features a coming-of-age story, with the title taken from the poem \"Non Sum Qualis eram Bonae Sub Regno Cynarae\", written by Ernest Dowson.",
                    CoverImage = "https://upload.wikimedia.org/wikipedia/en/6/6b/Gone_with_the_Wind_cover.jpg",
                    Publisher = "	Macmillan Publishers",
                    PublicationDate = "30 June 30 1936",
                    Category = "Romance",
                    ISBN = "978-0446365383",
                    PageCount = 1037,
                    CheckedOut = false
                },
                new Book
                {
                    ID = 4,
                    Title = "Jaws",
                    Author = "Peter Benchley",
                    Description = "Jaws is a novel by American writer Peter Benchley, published in 1974. It tells the story of a large great white shark that preys upon a small Long Island resort town and the three men who attempt to kill it. The novel grew out of Benchley's interest in shark attacks after he read about the exploits of Frank Mundus, a shark fisherman from Montauk, New York, in 1964. Doubleday commissioned him to write the novel in 1971, a period when Benchley worked as a freelance journalist.",
                    CoverImage = "https://upload.wikimedia.org/wikipedia/commons/5/5f/Jaws_%281974%29_front_cover%2C_first_edition.jpg",
                    Publisher = "Doubleday",
                    PublicationDate = "1 February 1974",
                    Category = "Horror",
                    ISBN = "978-0553085006",
                    PageCount = 278,
                    CheckedOut = false
                },
                new Book
                {
                    ID = 5,
                    Title = "The War of the Worlds",
                    Author = "H.G. Wells",
                    Description = "The War of the Worlds is a science fiction novel by English author H. G. Wells. It was written between 1895 and 1897, and serialised in Pearson's Magazine in the UK and Cosmopolitan magazine in the US in 1897. The full novel was first published in hardcover in 1898 by William Heinemann. The War of the Worlds is one of the earliest stories to detail a conflict between humankind and an extraterrestrial race.[3] The novel is the first-person narrative of an unnamed protagonist in Surrey and his younger brother who escapes to Tillingham in Essex as London and southern England is invaded by Martians. It is one of the most commented-on works in the science fiction canon.",
                    CoverImage = "https://upload.wikimedia.org/wikipedia/commons/3/30/The_War_of_the_Worlds_first_edition.jpg",
                    Publisher = "William Heinemann",
                    PublicationDate = "1 April 1898",
                    Category = "Sci-Fi",
                    ISBN = "978-1505260793",
                    PageCount = 287,
                    CheckedOut = false
                },
                new Book
                {
                    ID = 6,
                    Title = "Jurassic Park",
                    Author = "Michael Crichton",
                    Description = "Jurassic Park is a 1990 science fiction novel written by Michael Crichton. A cautionary tale about genetic engineering, it presents the collapse of a zoological park showcasing genetically recreated dinosaurs to illustrate the mathematical concept of chaos theory and its real-world implications. A sequel titled The Lost World, also written by Crichton, was published in 1995.",
                    CoverImage = "https://upload.wikimedia.org/wikipedia/en/2/21/Jurassic_Park_%28book_cover%29.jpg",
                    Publisher = "Alfred A. Knopf",
                    PublicationDate = "20 November 1990",
                    Category = "Sci-Fi",
                    ISBN = "978-0345538987",
                    PageCount = 399,
                    CheckedOut = false
                }
            );
            // // Look for any users.
            // if (context.User.Any())
            // {
            //     return;   // DB has been seeded
            // }
            // context.User.AddRange(
            //     new User
            //     {
            //         UserID = 0,
            //         UserName = "test1",
            //         Password = "12345",
            //         Category = Category.LIBRARIAN
            //     },                
            //     new User
            //     {
            //         UserID = 1,
            //         UserName = "test2",
            //         Password = "54321",
            //         Category = Category.CUSTOMER
            //     },                
            //     new User
            //     {
            //         UserID = 2,
            //         UserName = "test3",
            //         Password = "abcde",
            //         Category = Category.CUSTOMER
            //     }
            // );
            context.SaveChanges();
        }
    }
}