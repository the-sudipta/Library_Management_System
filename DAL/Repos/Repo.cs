using DAL.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
	internal class Repo
	{
		internal DatabaseContext db;
		internal Repo()
		{
			db = new DatabaseContext();
		}


		#region Text Color Configuration in CONSOLE
		public static void Print_in_Red(string text)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(text);
			Debug.WriteLine("***** ERROR: " + text + " *****");
			Console.ResetColor();
		}

		public static void Print_in_Green(string text)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(text);
			Debug.WriteLine("##### SUCCESS: " + text + " #####");
			Console.ResetColor();
		}




		#endregion Text Color Configuration in CONSOLE
	}
}
