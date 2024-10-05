using LibraryInventoryTracker.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;

//Create SQL database for storing data to be searched
String str = "CREATE DATABASE MyDatabase ON PRIMARY " +
 "(NAME = LibraryDB_Data, " +
 "FILENAME = 'C:\\LibraryDB_Data.mdf', " +
 "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%)" +
 "LOG ON (NAME = LibraryDB_Log, " +
 "FILENAME = 'C:\\LibraryDB.ldf', " +
 "SIZE = 1MB, " +
 "MAXSIZE = 5MB, " +
 "FILEGROWTH = 10%)";

string ConnString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\\LibraryDB_Data.mdf'; Integrated Security=True;Connect Timeout=30;Encrypt=False";

using (SqlConnection myConn = new SqlConnection (ConnString)) {
    SqlCommand myCommand = new SqlCommand(str, myConn);
    try {
        myConn.Open();
        myCommand.ExecuteNonQuery();
        Console.WriteLine("DataBase is Created Successfully");
    } catch (System.Exception ex) {
        Console.WriteLine("DataBase Creation Failed: " + ex.ToString());
    }
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<LibraryInventoryTrackerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryInventoryTrackerContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
