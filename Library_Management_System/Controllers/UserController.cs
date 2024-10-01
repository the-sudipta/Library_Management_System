using System.Web.Http.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Diagnostics;
using BLL.DTOs;

namespace Library_Management_System.Controllers
{
	[EnableCors("*", "*", "*")]
	[RoutePrefix("api/user")]
	public class UserController : ApiController
    {
		[HttpGet]
		[Route("all")]
		public HttpResponseMessage All_Users()
		{
			try
			{
				var authorizationHeader = Request.Headers.Authorization?.ToString() ?? "";
				if (int.TryParse(authorizationHeader, out int user_ID))
				{
					if (BLL.Services.UserService.Get(user_ID).Role == "Admin")
					{
						var data = BLL.Services.UserService.Get();
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
							Message = "Only Admin can request this api"
						};
						return Request.CreateResponse(HttpStatusCode.Unauthorized, responseMessage);
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
		[Route("profile")]
		public HttpResponseMessage UserByID()
		{
			try
			{
				var authorizationHeader = Request.Headers.Authorization?.ToString() ?? "";
				if (int.TryParse(authorizationHeader, out int user_ID))
				{
					var data = BLL.Services.UserService.Get(user_ID);
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

		[HttpPost]
		[Route("signup")]
		public HttpResponseMessage Create(UserDTO new_user) 
		{
			try
			{
				if (new_user != null)
				{
					var existing_user = BLL.Services.UserService.Get(new_user.Email);
					if (existing_user != null)
					{
						var responseMessage = new
						{
							Message = "User already exists"
						};
						return Request.CreateResponse(HttpStatusCode.Conflict, responseMessage);
					}
					else
					{
						bool isCreated = BLL.Services.UserService.Create(new_user);
						if (isCreated)
						{
							var responseMessage = new
							{
								Message = "Signup successful"
							};
							return Request.CreateResponse(HttpStatusCode.Created, responseMessage);
						}
						else
						{
							var responseMessage = new
							{
								Message = "Failed to signup"
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
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
			}

		}

		[HttpPost]
		[Route("login")]
		public HttpResponseMessage Login(LoginDTO login)
		{
			try
			{
				if (login != null)
				{
					int user_ID = BLL.Services.UserService.Login(login);
					if (user_ID > 0)
					{
						var responseMessage = new
						{
							Message = "Login successful",
							Token = user_ID
						};
						return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
					}
					else
					{
						var responseMessage = new
						{
							Message = "Login failed"
						};
						return Request.CreateResponse(HttpStatusCode.Unauthorized, responseMessage);
					}
				}
				else
				{
					var responseMessage = new
					{
						Message = "Please provide all the informations for user to login"
					};
					return Request.CreateResponse(HttpStatusCode.BadRequest, responseMessage);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
			}
		}


		[HttpPut]
		[Route("profile/update")]
		public HttpResponseMessage Update(UserDTO user)
		{
			try
			{
				if (user != null)
				{
					bool decision = BLL.Services.UserService.Update(user);
					if (decision)
					{
						return Request.CreateResponse(HttpStatusCode.OK, user);
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
						Message = "Please provide all the informations along with ID for user to update"
					};
					return Request.CreateResponse(HttpStatusCode.BadRequest, responseMessage);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = ex.Message });
			}
		}

		[HttpDelete]
		[Route("profile/delete")]
		public HttpResponseMessage Delete(UserDTO user)
		{
			try
			{
				if (user != null)
				{
					bool decision = BLL.Services.UserService.Delete(user.ID);
					if (decision)
					{
						var responseMessage = new
						{
							Message = "User deleted successfully"
						};
						return Request.CreateResponse(HttpStatusCode.OK, responseMessage);
					}
					else
					{
						var responseMessage = new
						{
							Message = "Delete falied! Internal Server issue"
						};
						return Request.CreateResponse(HttpStatusCode.InternalServerError, responseMessage);
					}
				}
				else
				{
					var responseMessage = new
					{
						Message = "Please provide all the informations along with ID for user to delete"
					};
					return Request.CreateResponse(HttpStatusCode.BadRequest, responseMessage);
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
