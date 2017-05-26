using System;
using System.Collections.Generic;
using GameConcepts.GameParser;



namespace ParserTest
{
	public class Program
	{
		public static void Main(string[] args)
		{
            var tec = new GameParser ();

			tec.ListLevels();
            tec.GetLevel (2);


			List<string> strl = tec.GetAsciiString;
			foreach (var letter in strl)
			{
				Console.Write(letter);
			}

            var nm = tec.GetLevelname;
            Console.WriteLine ();
            Console.WriteLine (nm);
            List<string> pltfm = tec.GetLevelPlatforms;
            foreach (var obj in pltfm) 
            {
                Console.WriteLine (obj);
            }
            var dict = tec.GetDictionaryFile;
            foreach (var obj in dict) 
            {
                Console.WriteLine (obj);
            }

            var rnd = new Random ();
            for (int i = 0; i < 100; i++) 
            {
                Console.WriteLine (rnd.Next (0, 3));
            }
		}
	}
}
