using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
	internal class BookRepo : Repo, IRepo<Book, int, bool>
	{
		public bool Create(Book obj)
		{
			try
			{
				db.Books.Add(obj);
				return db.SaveChanges() > 0;
			}
			catch (Exception ex)
			{
				Print_in_Red(ex.Message);
				return false;
			}
		}

		public List<Book> Read()
		{
			try
			{
				return db.Books.ToList();
			}
			catch (Exception ex)
			{
				Print_in_Red(ex.Message);
				return null;
			}
		}

		public Book Read(int id)
		{
			try
			{
				return db.Books.Find(id);
			}
			catch (Exception ex)
			{
				Print_in_Red("Book not Found. Error = " + ex.Message);
				return null;
			}
		}

		public bool Update(Book obj)
		{
			try
			{
				if (Read(obj.ID) != null)
				{
					db.Entry(Read(obj.ID)).CurrentValues.SetValues(obj);
					return db.SaveChanges() > 0;
				}
				else
				{
					Print_in_Red("Book not found for update");
					return false;
				}
			}
			catch (Exception ex)
			{
				Print_in_Red(ex.Message);
				return false;
			}
		}

		public bool Delete(int id)
		{
			try
			{
				if (Read(id) != null)
				{
					db.Books.Remove(Read(id));
					return db.SaveChanges() > 0;
				}
				else
				{
					Print_in_Red("Book not found for delete");
					return false;
				}
			}
			catch (Exception ex)
			{
				Print_in_Red(ex.Message);
				return false;
			}
		}
	}
}
