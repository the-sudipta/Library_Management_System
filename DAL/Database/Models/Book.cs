using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
	public class Book
	{
		[Key]
		public int ID { get; set; }

        [Required]
		// if we do not specify the length, it will be max length
		public string Title { get; set; }

        [Required]
        public string Author { get; set; }

		[Required]
		public string Genre { get; set; }

        [Required]
        public DateTime Publication_Date { get; set; }


		#region Relationship

		/// <summary>
		/// A user can have many books
		/// Book is the `Many' side of the relationship
		/// </summary>
		[ForeignKey("User")]
		// Here if we use like : int?, then it means `Null Allowed'
		public int User_ID { get; set; }
		public virtual User User { get; set; }


		#endregion Relationship


		#region Postman Example
		/*
		 * Postman Example:
         {
              "title": "The Great Gatsby",
              "author": "F. Scott Fitzgerald",
              "genre": "Classic",
              "publication_Date": "1925-04-10"
         } 
         */
		#endregion Postman Example
	}
}
