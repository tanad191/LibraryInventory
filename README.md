# Library Inventory README

### Take-Home Project for Troy Web Consulting Application

This project was created in Visual Studio Code (v1.94.2), using ASP.NET Core 8.0 using C# with MVC design, Entity Framework (v6.5.1), Razor (v3.3.0), SQLite (v8.0.8), Newtonsoft JSON (v13.0.3), CSHTML, and JavaScript.

## REQUIRED INSTALLS:
- Visual Studio Code (v1.94.2)
- SQL Server 2022 Express Edition (v16.0.1000.6)

## REQUIRED EXTENSIONS IN VISUAL STUDIO CODE:
- C# Dev Kit (v1.11.14)
- SQL Server (mssql) (v1.24.0) 

## HOW TO INSTALL:
1. Install both of the programs listed under "**REQUIRED INSTALLS**". After installing SQL Server Express, copy and save the local connection string for a later step in this installation process - the format for this string should be similar to the following: *server=localhost\SQLEXPRESS01;Database=master;Trusted_Connection=True;*
2. Clone this repository from GitHub to a directory of your choice - in this example, I'm using "*C:\GitHub\LibraryInventory*". In the *Program.cs* file, located in the *\LibraryInventoryTracker* folder, modify the connection string in Line 10 (*SqlConnection ConnString1 = new SqlConnection(@"{server=(LocalDB)\MSSQLLocalDB}");*) by replacing the {bracketed text} with the local connection string for SQL Server Express that you obtained in the first step.
3. Open Visual Studio Code and open this folder in it, then open a new terminal - it should have already navigated to this directory, but otherwise use the command "*cd C:\GitHub\LibraryInventory*" to navigate there manually.
4. In the Extensions menu, search for the extensions listed under "**REQUIRED EXTENSION IN VISUAL STUDIO CODE**" and install them in Visual Studio Code.
5. Navigate to the main project directory with the command "*cd LibraryInventoryTracker*" or "*cd C:\GitHub\LibraryInventory\LibraryInventoryTracker*" for package installation. From this directory, run the following commands on the terminal:
```
dotnet tool install --global dotnet-ef
dotnet add package EntityFramework
dotnet add package Microsoft.AspNetCore.Http
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 8.0.8
dotnet add package Microsoft.Extensions.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SQLite
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Newtonsoft.Json
dotnet add package System.Web.Optimization
```
6. Using your local explorer, create a new folder in the *C:\\* directory and name it "*temp*". This will be where the SQLite database objects will be kept when this project is run.
7. Open *Program.cs* in the *LibraryInventoryTracker* directory, and then run this file to start the program. The database files should be automatically created in the *C:\temp* folder once the program starts.

## HOW TO USE THIS APPLICATION:
1. The front page includes a randomized selection of up to 3 books. To view the full inventory, click the "Archive" link on the top right of the page. Books in the archive can only be checked out by a logged-in user (either customer or librarian), and only created, returned, edited, or deleted by a logged-in librarian.
2. Create a sample user by clicking the "Register" link on the top right of the page. Enter a sample name and password and select the desired category. Note that the user will not be signed in upon being created; to sign in, click the "Sign Up" link and enter the username and password used to create the account.
3. Once signed in, you can sign out by clicking the "Sign Out" link on the upper right (which replaces the "Sign In" link automatically when logged in).
4. Both signing in via the Login page and signing out automatically take you to the homepage, meaning you'll have to manually navigate back to the page you intend to inspect if you were viewing a different page in this application.
  - To check any functionality on either the Index page or the Details page of any book in the archive whose visibility is dependent on the category of the signed-in user: sign into a Customer account and view the page, sign out, and sign into a Librarian account before viewing the page again (or vice versa).
5. In the /Book page, the "Create New" button and the "Delete", "Edit", and "Return" links for each book row, along with their respective functionalities, can only be accessed by a signed-in librarian. "Checkout" is accessible by signed-in users of either category. "Details" can be accessed even when signed out, and will take you to the page /Book/Details/{id}.
  - In the /Book/Details/{id} page, the "Check Out" link is only visible to signed-in users and only if the book has not been checked out already. Otherwise, the "Return" link is only visible to signed-in librarians, as is the "Edit" link.
6. To check the list of registered accounts, go to the homepage and add /User to the end of the URL. Only accounts with the Librarian category can access this page and create/edit users, but if you are not signed in or are signed in as a customer, entering this URL will redirect you to the /Home/Error page.

### TESTING DATA:
Here is the sample data I used when testing my application. You can use any placeholder data you want for testing purposes, but For testing purposes, it's recommended to create at least one account each with the Customer and Librarian categories. This way, you can check what is visible/accessible or otherwise for either of the two categories.

**Data for testing user creation:**
- Name = test01; Password = abcd; Category = CUSTOMER
- Name = test02; Password = 1234; Category = LIBRARIAN

**Data for testing book creation:**
- Title = Tarzan of the Apes
- Author = Edgar Rice Burroughs
- Description = Tarzan of the Apes is a 1912 novel by American writer Edgar Rice Burroughs, and the first in the Tarzan series. The story was first printed in the pulp magazine The All-Story in October 1912 before being released as a novel in June 1914. The story follows the title character Tarzan's adventures, from his childhood being raised by apes in the jungle to his eventual encounters with other humans and Western society. So popular was the character that Burroughs continued the series into the 1940s with two dozen sequels.
- Cover Illustration = https://upload.wikimedia.org/wikipedia/commons/4/48/Tarzan_of_the_Apes_in_color.jpg
- Publisher = A. C. McClurg
- Publication Date = October 1, 1912
- Category = Adventure
- ISBN = 978-0345319777
- Page Count = 400
