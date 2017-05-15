using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserTest
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var tec = new GameParser();
			tec.GetLevel(1);
			tec.ListLevel();
			List<string> strl = tec.StringList;
			foreach (var letter in strl)
			{
				Console.Write(letter);
			}
		}
	}
}
