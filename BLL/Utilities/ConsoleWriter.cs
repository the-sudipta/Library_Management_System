using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utilities
{
	public class ConsoleWriter
	{
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
