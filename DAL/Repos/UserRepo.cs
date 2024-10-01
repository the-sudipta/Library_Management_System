using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
	internal class UserRepo : Repo, IRepo<User, int, bool>
	{
		public bool Create(User obj)
		{
			try
			{
				db.Users.Add(obj);
				return db.SaveChanges() > 0;
			}
			catch (Exception ex)
			{
				Print_in_Red(ex.Message);
				return false;
			}
		}

		public List<User> Read()
		{
			try
			{
				return db.Users.ToList();
			}
			catch (Exception ex)
			{
				Print_in_Red(ex.Message);
				return null;
			}
		}

		public User Read(int id)
		{
			try
			{
				return db.Users.Find(id);
			}
			catch (Exception ex)
			{
				Print_in_Red("User not Found. Error = " + ex.Message);
				return null;
			}
		}

		public User ReadByEmail(string email)
		{
			try
			{
				return db.Users.FirstOrDefault(u => u.Email == email);
			}
			catch (Exception ex)
			{
				Print_in_Red("User not Found based on the email = "+ email + ". UserRepo Error = " + ex.Message);
				return null;
			}
		}

		public bool Update(User obj)
		{
			try
			{
				if(Read(obj.ID) != null)
				{
					db.Entry(Read(obj.ID)).CurrentValues.SetValues(obj);
					return db.SaveChanges() > 0;
				}
				else
				{
					Print_in_Red("User not found for update");
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
					db.Users.Remove(Read(id));
					return db.SaveChanges() > 0;
				}
				else
				{
					Print_in_Red("User not found for delete");
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
