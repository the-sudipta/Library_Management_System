using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
	public class UserDTO
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(50, ErrorMessage = "Password must be at most 50 characters long")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Role is required")]
		public string Role { get; set; }
	}
}
