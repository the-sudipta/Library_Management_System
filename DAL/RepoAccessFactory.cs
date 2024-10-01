using DAL.Interfaces;
using DAL.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
	public class RepoAccessFactory
	{
		public static IRepo<User, int, bool> UserData()
		{
			return new UserRepo();
		}

		public static IRepo<Book, int, bool> BookData()
		{
			return new BookRepo();
		}

		
	}
}
