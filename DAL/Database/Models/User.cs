using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
	public class User
	{
		[Key]
		public int ID { get; set; }

		[Required]
		[StringLength(50)]
		public string Email { get; set; }
		[Required]
		[StringLength(50)]
		public string Password { get; set; }
		[Required]
		// if we do not specify the length, it will be max length
		public string Role { get; set; }


		#region Relationship

		/// <summary>
		/// A user can have many books
		/// User is the `One' side of the relationship
		/// </summary>
		public virtual ICollection<Book> Books { get; set; }

		public User()
		{
			Books = new List<Book>();
		}


		#endregion Relationship


		#region Postman Example
		/*
		 * Postman Example:
		{
			"Email": "user@example.com",
			"Password": "securepassword123",
			"Role": "Admin"
		}
		 */
		#endregion Postman Example


	}
}
