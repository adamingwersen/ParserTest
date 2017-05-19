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
			tec.ListLevels();
            tec.GetLevel (1);
			List<string> strl = tec.GetStringList;
			foreach (var letter in strl)
			{
				Console.Write(letter);
			}

            var nm = tec.GetLevelname;
            Console.WriteLine (nm);
            var pltfm = tec.GetLevelPlatforms;
            Console.WriteLine (pltfm);

            var dict = tec.GetDictionaryFile;
            foreach (var obj in dict) 
            {
                Console.WriteLine (obj);
            }



		}
	}
}
