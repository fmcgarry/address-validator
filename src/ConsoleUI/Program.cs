using System;
using System.Net.Http;

namespace ConsoleUI
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			ValidationAPI.Core core = new ValidationAPI.Core();

			core.Run();

			Console.ReadKey();
		}
	}
}