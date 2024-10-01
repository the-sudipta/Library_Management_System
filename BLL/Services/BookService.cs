using BLL.DTOs;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Utilities;

namespace BLL.Services
{
	public class BookService
	{
		public static List<BookDTO> Get()
		{
			var data = RepoAccessFactory.UserData().Read();
			if (data.Count > 0)
			{
				/// MapperService<THE_BASE_TYPE_THAT_I_DON'T_WANT , THE_FINAL_OUTPUT_TYPE_THAT_I_WANT>.GetMapper();
				var mapper = MapperService<Book, BookDTO>.GetMapper();
				var book_list = mapper.Map<List<BookDTO>>(data);
				return book_list;
			}
			else
			{
				Debug.WriteLine("No data found in Book Table");
				return null;
			}

		}

		public static BookDTO Get(int id)
		{
			if (id > 0)
			{
				var data = RepoAccessFactory.BookData().Read(id);
				if (data != null)
				{
					var mapper = MapperService<Book, BookDTO>.GetMapper();
					var book = mapper.Map<BookDTO>(data);
					return book;
				}
				else
				{
					Debug.WriteLine("No book data found for the given ID");
					return null;
				}
			}
			else
			{
				Debug.WriteLine("ID is less than zero which is not acceptable");
				return null;
			}
		}

		public static List<BookDTO> GetBooksByUserID(int id)
		{
			if (id > 0)
			{
				List<Book> data = RepoAccessFactory.BookData().Read().Where(book => book.User_ID == id).ToList();
				var mapper = MapperService<Book, BookDTO>.GetMapper();
				var book_list = mapper.Map<List<BookDTO>>(data);
				return book_list.Count > 0? book_list : null;
			}
			else
			{
				Debug.WriteLine("ID is less than zero which is not acceptable");
				return null;
			}
		}

		public static BookDTO GetBooksByBookIDAndUserID(int book_id, int user_id)
		{
			if (book_id > 0 && user_id > 0)
			{
				Book data = RepoAccessFactory.BookData().Read().FirstOrDefault(book => book.ID == book_id && book.User_ID == user_id);
				var mapper = MapperService<Book, BookDTO>.GetMapper();
				var book_data = mapper.Map<BookDTO>(data);
				return book_data;
			}
			else
			{
				Debug.WriteLine("Book_ID or User_ID is less than zero which is not acceptable");
				return null;
			}
		}

		public static List<BookDTO> GetBooksByAuthor(string author)
		{
			if (author != null)
			{
				List<Book> data = RepoAccessFactory.BookData().Read().Where(book => book.Author == author).ToList();
				var mapper = MapperService<Book, BookDTO>.GetMapper();
				var book_list = mapper.Map<List<BookDTO>>(data);
				return book_list.Count > 0 ? book_list : null;
			}
			else
			{
				ConsoleWriter.Print_in_Red("Author name is not provided which is not acceptable");
				return null;
			}
		}

		public static BookDTO GetBooksByTitle(string title)
		{
			if (title != null)
			{
				Book book_data = RepoAccessFactory.BookData().Read().FirstOrDefault(book => book.Title == title);

				if (book_data != null)
				{
					var mapper = MapperService<Book, BookDTO>.GetMapper();
					var book = mapper.Map<BookDTO>(book_data);
					return book;
				}
				else
				{
					ConsoleWriter.Print_in_Red("Book not found. From BookService");
					return null;
				}
			}
			else
			{
				ConsoleWriter.Print_in_Red("No title provided. From BookService");
				return null;
			}

		}

		public static bool Create(BookDTO book)
		{
			if (book != null)
			{
				var mapper = MapperService<BookDTO, Book>.GetMapper();
				var book_data = mapper.Map<Book>(book);
				return RepoAccessFactory.BookData().Create(book_data);
			}
			else
			{
				Debug.WriteLine("Book object is null");
				return false;
			}
		}

		public static bool Update(BookDTO book)
		{
			if (book != null)
			{
				var mapper = MapperService<BookDTO, Book>.GetMapper();
				var book_data = mapper.Map<Book>(book);
				return RepoAccessFactory.BookData().Update(book_data);
			}
			else
			{
				Debug.WriteLine("Book object is null");
				return false;
			}
		}

		public static bool Delete(int id)
		{
			if (id > 0)
			{
				return RepoAccessFactory.BookData().Delete(id);
			}
			else
			{
				ConsoleWriter.Print_in_Red("ID is less than zero which is not acceptable");
				return false;
			}
		}


	}
}
