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
            TextReader rd = tec.GetReader;
			tec.GetLevel(rd);
            tec._level = 1;
			tec.ListLevels();
			List<string> strl = tec.StringList;
            Console.WriteLine(strl[40]);
			foreach (var letter in strl)
			{
				Console.Write(letter);
			}
            tec.PopulateDictionary ();
			//var dct = tec.GetDictionary;
			//foreach (var keyval in dct) 
			//{
			//    Console.WriteLine (keyval);
			//}
			foreach (var obj in tec.paths)
			{
				Console.WriteLine(obj);
			}
			foreach (var obj in tec.ident)
			{
				Console.WriteLine(obj);
			}
		}
	}
}
