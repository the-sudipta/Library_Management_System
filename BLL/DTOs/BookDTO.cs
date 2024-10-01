using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
	public class BookDTO
	{
		public int ID { get; set; }

		[Required(ErrorMessage = "The title is required")]
		[StringLength(100, ErrorMessage = "The title cannot exceed 100 characters")]
		public string Title { get; set; }

		[Required(ErrorMessage = "The author's name is required")]
		[StringLength(60, ErrorMessage = "The author's name cannot exceed 60 characters")]
		public string Author { get; set; }

		[Required(ErrorMessage = "The genre is required")]
		[StringLength(50, ErrorMessage = "The genre cannot exceed 50 characters")]
		public string Genre { get; set; }

		[Required(ErrorMessage = "The publication date is required")]
		[DataType(DataType.Date, ErrorMessage = "The publication date must be a valid date")]
		[CustomValidation(typeof(BookDTO), "ValidatePublicationDate")]
		public DateTime Publication_Date { get; set; }

		[Required(ErrorMessage = "The user id for the book is required")]
		public int User_ID { get; set; }

		#region Custom Validation

		// Custom validation for the publication date to ensure it's not a future date
		public static ValidationResult ValidatePublicationDate(DateTime publicationDate, ValidationContext context)
		{
			if (publicationDate > DateTime.Now)
			{
				return new ValidationResult("The publication date cannot be in the future");
			}
			return ValidationResult.Success;
		}
		#endregion Custom Validation
	}
}
