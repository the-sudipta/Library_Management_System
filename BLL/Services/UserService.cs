using BLL.DTOs;
using BLL.Utilities;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class UserService
	{
		public static List<UserDTO> Get()
		{
			var data = RepoAccessFactory.UserData().Read();
			if (data.Count > 0)
			{
				/// MapperService<THE_BASE_TYPE_THAT_I_DON'T_WANT , THE_FINAL_OUTPUT_TYPE_THAT_I_WANT>.GetMapper();
				var mapper = MapperService<User, UserDTO>.GetMapper();
				var user_list = mapper.Map<List<UserDTO>>(data);
				return user_list;
			}
			else
			{
				Debug.WriteLine("No data found in User Table");
				return null;
			}

		}

		public static UserDTO Get(int id)
		{
			if (id > 0)
			{
				var data = RepoAccessFactory.UserData().Read(id);
				if (data != null)
				{
					var mapper = MapperService<User, UserDTO>.GetMapper();
					var user = mapper.Map<UserDTO>(data);
					return user;
				}
				else
				{
					Debug.WriteLine("No data found for the given ID");
					return null;
				}
			}
			else
			{
				Debug.WriteLine("ID is less than zero which is not acceptable");
				return null;
			}
		}

		public static bool Create(UserDTO user)
		{
			if (user != null)
			{
				var mapper = MapperService<UserDTO, User>.GetMapper();
				var user_data = mapper.Map<User>(user);
				return RepoAccessFactory.UserData().Create(user_data);
			}
			else
			{
				
				ConsoleWriter.Print_in_Red("User object is null");
				return false;
			}
		}

		public static bool Update(UserDTO user)
		{
			if (user != null)
			{
				var mapper = MapperService<UserDTO, User>.GetMapper();
				var user_data = mapper.Map<User>(user);
				return RepoAccessFactory.UserData().Update(user_data);
			}
			else
			{
				Debug.WriteLine("User object is null");
				return false;
			}
		}

		public static bool Delete(int id)
		{
			if (id > 0)
			{
				return RepoAccessFactory.UserData().Delete(id);
			}
			else
			{
				Debug.WriteLine("ID is less than zero which is not acceptable");
				return false;
			}
		}

		public static int Login(LoginDTO login)
		{
			if (login != null)
			{
				User user_data = RepoAccessFactory.UserData().Read().FirstOrDefault(user => user.Email == login.Email);

				if (user_data != null)
				{
					Debug.WriteLine("Password inside the Database = " + login.Password);
					return user_data.Password == login.Password ? user_data.ID : -1;
				}
				else
				{
					Debug.WriteLine("User not found");
					return -1;
				}
			}
			else
			{
				Debug.WriteLine("No login data provided");
				return -1;
			}

		}

		public static UserDTO Get(string email)
		{
			User user_data = RepoAccessFactory.UserData().Read().FirstOrDefault(user => user.Email == email);
			if (user_data != null)
			{
				var mapper = MapperService<User, UserDTO>.GetMapper();
				var user = mapper.Map<UserDTO>(user_data);
				return user;
			}
			else
			{
				Debug.WriteLine("No data found for the given Email");
				return null;
			}


		}
	}
}
