namespace DAL.Migrations
{
	using DAL.Models;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.Database.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DAL.Database.DatabaseContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method
			//  to avoid creating duplicate seed data.

			// Inserting one User

			#region Data Insertion
			//var user = new User
			//{
			//	Email = "test1@gmail.com",
			//	Password = "admin",
			//	Role = "Admin"
			//};

			//// Adding user to the context
			//context.Users.AddOrUpdate(u => u.Email, user);
			//context.SaveChanges(); // Save the user first to get the User_ID for the books

			//// Inserting 10 books for the user
			//var books = new List<Book>
			//{
			//	new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Classic", Publication_Date = new DateTime(1925, 4, 10), User_ID = user.ID },
			//	new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Publication_Date = new DateTime(1960, 7, 11), User_ID = user.ID },
			//	new Book { Title = "1984", Author = "George Orwell", Genre = "Dystopian", Publication_Date = new DateTime(1949, 6, 8), User_ID = user.ID },
			//	new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", Publication_Date = new DateTime(1813, 1, 28), User_ID = user.ID },
			//	new Book { Title = "The Catcher in the Rye", Author = "J.D. Salinger", Genre = "Fiction", Publication_Date = new DateTime(1951, 7, 16), User_ID = user.ID },
			//	new Book { Title = "Moby-Dane", Author = "Herman Melville", Genre = "Adventure", Publication_Date = new DateTime(1851, 11, 14), User_ID = user.ID },
			//	new Book { Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Genre = "Fantasy", Publication_Date = new DateTime(1954, 7, 29), User_ID = user.ID },
			//	new Book { Title = "The Hobbit", Author = "J.R.R. Tolkien", Genre = "Fantasy", Publication_Date = new DateTime(1937, 9, 21), User_ID = user.ID },
			//	new Book { Title = "War and Peace", Author = "Leo Tolstoy", Genre = "Historical", Publication_Date = new DateTime(1869, 1, 1), User_ID = user.ID },
			//	new Book { Title = "Crime and Punishment", Author = "Fyodor Dostoevsky", Genre = "Psychological Fiction", Publication_Date = new DateTime(1866, 1, 1), User_ID = user.ID }
			//};

			//// Adding books to the context
			//books.ForEach(b => context.Books.AddOrUpdate(b));
			//context.SaveChanges(); // Save changes to commit the data to the database
			#endregion Data Insertion


		}
	}
}
