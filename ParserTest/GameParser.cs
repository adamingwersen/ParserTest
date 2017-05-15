using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParserTest
{
	public class GameParser
	{
		/// <summary>
		/// A list of characters (string-format).
		/// </summary>
		protected readonly List<string> _stringList = new List<string>();

		/// <summary>
		/// The predefined dimensions of the ASCII-art image
		/// </summary>
		private int _levelHeight = 23;
		private int _levelWidth = 40;

		/// <summary>
		/// Retrives the files in the Levels-directory
		/// </summary>
		/// <returns>The files present text-files in the directory</returns>
		private string[] GetFiles()
		{
			string[] dirContents = null; 
			try
			{
				var currentDir = Directory.GetCurrentDirectory();
				var levelsDir = Path.Combine(currentDir, "Levels");
				dirContents = Directory.GetFiles(levelsDir, "*.txt");
				return dirContents;
			}
			catch (UnauthorizedAccessException e)
			{
				Console.WriteLine(e.Message);
			}
			if (dirContents != null)
			{
				return dirContents;
			}
			else {
				Console.WriteLine("There's nothing contained in the specified folder");
				return dirContents;
			}
		}

		public void ListLevel()
		{
			var files = GetFiles();
			Console.WriteLine("Choose a level:");
			for (int i = 0; i < files.Length; i++)
			{
				
				Console.Write(i + ")");
				Console.Write("{i}", files);
				Console.WriteLine("");
			}
		}

		/// <summary>
		/// Retrieves an ASCII-art image and returns this as a string-list. 
		/// </summary>
		/// <returns>A level to be translated</returns>
		/// <param name="level">A level - another method specifies the name and number</param>
		public List<string> GetLevel(int level)
		{
			var files = GetFiles();
			try
			{
				using (TextReader reader = File.OpenText(files[level]))
				{
					for (int i = 0; i < (_levelWidth * _levelHeight) + _levelHeight; i++)
					{
						int integer = reader.Read();

						if (integer == -1)
							break;

						char character = (char)integer;
						_stringList.Add(character.ToString());
					}
				}
				return _stringList;
			}
			catch (IndexOutOfRangeException e)
			{
				string msg = e.Message;
				Console.WriteLine("{0} Either the text-file does not contain a level - otherwise the level is incorrectly formatted", msg);
				return _stringList;
			}
		}


		public List<string> StringList
		{
			get { return _stringList; }
		}
	}
}
