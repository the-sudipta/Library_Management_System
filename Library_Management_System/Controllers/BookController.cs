using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Library_Management_System.Controllers
{
	[EnableCors("*", "*", "*")]
	[RoutePrefix("api/book")]
	public class BookController : ApiController
    {

		[HttpGet]
		[Route("all")]
		public HttpResponseMessage All_Books()
		{
			try
			{
				var authorizationHeader = Request.Headers.Authorization?.ToString() ?? "";
				if (int.TryParse(authorizationHeader, out int user_ID))
				{
					var data = BLL.Services.BookService.GetBooksByUserID(user_ID);
					if (data != null)
					{
						return Request.CreateResponse(HttpStatusCode.OK, data);
					}
					else
					{
						var responseMessage = new
						{
							Message = "No data available"
						};
						return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
					}
				}
				else
				{
					var responseMessage = new
					{
						Message = "Token Not Found"
					};
					return Request.CreateResponse(HttpStatusCode.Unauthorized, responseMessage);
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
			}
		}

		[HttpGet]
		[Route("{id}")]
		public HttpResponseMessage BookByID(int id)
		{
			try
			{
				var data = BLL.Services.BookService.Get(id);
				if (data != null)
				{
					return Request.CreateResponse(HttpStatusCode.OK, data);
				}
				else
				{
					var responseMessage = new
					{
						Message = "No data available"
					};
					return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
			}
		}

		[HttpGet]
		[Route("search/{search_term}")]
		public HttpResponseMessage SearchBookByAuthorAndTitle(string search_term)
		{
			try
			{
				var book_by_title = BLL.Services.BookService.GetBooksByTitle(search_term);
				if (book_by_title != null)
				{
					return Request.CreateResponse(HttpStatusCode.OK, book_by_title);
				}
				else
				{
					var books_by_author = BLL.Services.BookService.GetBooksByAuthor(search_term);
					if (books_by_author != null)
					{
						return Request.CreateResponse(HttpStatusCode.OK, books_by_author);
					}
					else
					{
						var responseMessage = new
						{
							Message = "No data available"
						};
						return Request.CreateResponse(HttpStatusCode.NotFound, responseMessage);
					}
				}

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
			}
		}

		[HttpPost]
		[Route("new")]
		public HttpResponseMessage Create(BookDTO new_book)
		{
			try
			{
				var authorizationHeader = Request.Headers.Authorization?.ToString() ?? "";
				if (int.TryParse(authorizationHeader, out int user_ID))
				{
					if (new_book != null)
					{
						var existing_book_test_1 = BLL.Services.BookService.GetBooksByAuthor(new_book.Author);
						var existing_book_test_2 = BLL.Services.BookService.GetBooksByTitle(new_book.Title);
						if (existing_book_test_1 != null && existing_book_test_2 != null)
						{
							var responseMessage = new
							{
								Message = "Book already exists"
							};
							return Request.CreateResponse(HttpStatusCode.Conflict, responseMessage);
						}
						else
						{
							new_book.User_ID = user_ID;
							bool isCreated = BLL.Services.BookService.Create(new_book);
							if (isCreated)
							{
								var responseMessage = new
								{
									Message = "Book added successfully"
								};
								return Request.CreateResponse(HttpStatusCode.Created, responseMessage);
							}
							else
							{
								var responseMessage = new
								{
									Message = "Failed to add new book"
								};
								return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
							}
						}
					}
					else
					{
						var responseMessage = new
						{
							Message = "Please provide all the informations for user to signup"
						};
						return Request.CreateResponse(HttpStatusCode.BadRequest, responseMessage);
					}
				}
				else
				{
					var responseMessage = new
					{
						Message = "Token Not Found"
					};
					return Request.CreateResponse(HttpStatusCode.Unauthorized, responseMessage);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
			}

		}

		[HttpPut]
		[Route("update")]
		public HttpResponseMessage Update(BookDTO book)
		{
			try
			{
				var authorizationHeader = Request.Headers.Authorization?.ToString() ?? "";
				if (int.TryParse(authorizationHeader, out int user_ID)) 
				{
					if (book != null)
					{

						BookDTO existing_book = BLL.Services.BookService.GetBooksByBookIDAndUserID(book.ID, user_ID);
						UserDTO current_user = BLL.Services.UserService.Get(user_ID);
						//Debug.WriteLine(existing_book.Title);
						//Debug.WriteLine(current_user.Role);

						if (existing_book != null || current_user.Role == "Admin")
						{
							bool decision = BLL.Services.BookService.Update(book);
							if (decision)
							{
								return Request.CreateResponse(HttpStatusCode.OK, book);
							}
							else
							{
								var responseMessage = new
								{
									Message = "Update falied! Internal Server issue"
								};
								return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
							}
						}
						else
						{
							var responseMessage = new
							{
								Message = "You do not have any rights to change the book information"
							};
							return Request.CreateResponse(HttpStatusCode.Unauthorized, responseMessage);
						}
					}
					else
					{
						var responseMessage = new
						{
							Message = "Please provide all the informations along with ID for book to update"
						};
						return Request.CreateResponse(HttpStatusCode.BadRequest, responseMessage);
					}
				}
				else
				{
					var responseMessage = new
					{
						Message = "Token Not Found"
					};
					return Request.CreateResponse(HttpStatusCode.Unauthorized, responseMessage);
				}
				
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
			}
		}

		[HttpDelete]
		[Route("delete/{book_id}")]
		public HttpResponseMessage Delete(int book_id)
		{
			try
			{
				var authorizationHeader = Request.Headers.Authorization?.ToString() ?? "";
				if (int.TryParse(authorizationHeader, out int user_ID)) 
				{
					if (book_id > 0)
					{

						BookDTO existing_book = BLL.Services.BookService.GetBooksByBookIDAndUserID(book_id, user_ID);
						UserDTO current_user = BLL.Services.UserService.Get(user_ID);

						if (existing_book != null || current_user.Role == "Admin")
						{
							bool decision = BLL.Services.BookService.Delete(book_id);
							if (decision)
							{
								var responseMessage = new
								{
									Message = "Book deleted successfully"
								};
								return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
							}
							else
							{
								var responseMessage = new
								{
									Message = "Failed to delete! Internal Server issue"
								};
								return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
							}
						}
						else
						{
							var responseMessage = new
							{
								Message = "You do not have any rights to delete the book"
							};
							return Request.CreateResponse(HttpStatusCode.Unauthorized, responseMessage);
						}

					}
					else
					{
						var responseMessage = new
						{
							Message = "Please provide ID for book to delete"
						};
						return Request.CreateResponse(HttpStatusCode.BadRequest, responseMessage);
					}
				} 
				else 
				{
					var responseMessage = new
					{
						Message = "Token Not Found"
					};
					return Request.CreateResponse(HttpStatusCode.Unauthorized, responseMessage);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
			}
		}



	}
}
