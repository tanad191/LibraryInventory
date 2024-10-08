using LibraryInventoryTracker.Data;
using LibraryInventoryTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

//Create SQL database for storing data to be searched
SqlConnection ConnString1 = new SqlConnection(@"server=(LocalDB)\MSSQLLocalDB");
ConnString1.Open();

string sql = string.Format(@"
    IF EXISTS(SELECT * FROM sys.databases WHERE name = 'Library')
    BEGIN
        DROP DATABASE [Library]
    END

    BEGIN
        CREATE DATABASE
            [Library]
        ON PRIMARY (
        NAME=LibraryDB_data,
        FILENAME = '{0}\LibraryDB_data.mdf'
        )
        LOG ON (
            NAME=LibraryDB_log,
            FILENAME = '{0}\LibraryDB_log.ldf'
        )

        DROP TABLE IF EXISTS Book;

        CREATE TABLE Book (
            ID INTEGER PRIMARY KEY,
            Title TEXT NOT NULL,
            Author TEXT NOT NULL,
            Description TEXT NOT NULL,
            CoverImage TEXT NOT NULL,
            Publisher TEXT NOT NULL,
            PublicationDate TEXT NOT NULL,
            Category TEXT NOT NULL,
            ISBN TEXT NOT NULL,
            PageCount INTEGER NOT NULL,
            CheckedOut INTEGER DEFAULT 0,
            CheckoutID INTEGER NULL
        );

        DROP TABLE IF EXISTS [User];

        CREATE TABLE [User] (
            UserID INTEGER PRIMARY KEY,
            UserName TEXT NOT NULL,
            Password TEXT NOT NULL,
            Category INTEGER DEFAULT 1,
            IsActive INTEGER DEFAULT 0
        );
    END",
    @"C:\sqlite"
);

SqlCommand command = new SqlCommand(sql, ConnString1);

try {
    command.ExecuteNonQuery();
    Console.WriteLine("DataBase is Created Successfully");
} catch (System.Exception ex) {
    Console.WriteLine("DataBase Creation Failed: " + ex.ToString());
}

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibraryInventoryTrackerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("LibraryInventoryTrackerContext") ?? throw new InvalidOperationException("Connection string 'LibraryInventoryTrackerContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(600);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    
    SeedData.Initialize(services);
}

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
