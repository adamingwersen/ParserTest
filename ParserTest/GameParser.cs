using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
            
namespace ParserTest
{
	public class GameParser
	{
		/// <summary>
		/// A list of characters (string-format).
		/// </summary>
		protected readonly List<string> _stringList = new List<string>();

        /// <summary>
        /// A dictionary containing key/value-pairs for ASCII-character to Path to image. 
        /// </summary>
        protected readonly Dictionary<string, string> _levelDictionary = new Dictionary<string, string> ();

		/// <summary>
		/// The predefined dimensions of the ASCII-art image
		/// </summary>
		private int _levelHeight = 23;
		private int _levelWidth = 40;

        /// <summary>
        /// The level to be used
        /// </summary>
        public int _level;

        private TextReader _reader;

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


        /// <summary>
        /// Presents user with available levels in directory
        /// </summary>
		public void ListLevels()
		{
			var files = GetFiles();
			Console.WriteLine("Choose a level:");
			for (int i = 0; i < files.Length; i++)
			{
				
				Console.Write(i + ")");
				Console.Write(files[i]);
				Console.WriteLine("");
			}
		}

		/// <summary>
		/// Retrieves an ASCII-art image and returns this as a string-list. 
		/// </summary>
		/// <returns>A level to be translated</returns>
		/// <param name="_level">A level - another method specifies the name and number</param>

		public void GetLevel(TextReader reader)
		{
			var files = GetFiles();
			try
			{
				using (reader = File.OpenText(files[_level]))
				{
					for (int i = 0; i < (_levelWidth * _levelHeight) + _levelHeight; i++)
					{
						int integer = reader.Read();

						if (integer == -1)
							break;

						char character = (char)integer;
                        string letter = character.ToString();

                        if(letter != "\n")
                        {
                            _stringList.Add(letter);
                        }
					}
                    _reader = reader;
				}
			}
			catch (IndexOutOfRangeException e)
			{
				string msg = e.Message;
				Console.WriteLine("{0} Either the text-file does not contain a level - otherwise the level is incorrectly formatted", msg);
			}
		}


        public void FillDictionary(TextReader reader)
        {
            string identifierPattern = @"(^.)";
            Regex identifierRgx = new Regex (identifierPattern, RegexOptions.IgnoreCase);
            string pathPattern = @"((?<=[)])\s).*+$";
            Regex pathRgx = new Regex (pathPattern, RegexOptions.IgnoreCase);
            reader.ReadLine ().Skip (4);

            string line;
            while ((line = reader.ReadLine ()) != null) {
                string identifier = identifierRgx.Match (line).ToString ();
                string path = pathRgx.Match (line).ToString ();
                _levelDictionary.Add (identifier, path);
            }
        }



        /// <summary>
        /// Gets the string list.
        /// </summary>
        /// <value>The string list.</value>
		public List<string> StringList
		{
			get { return _stringList; }
		}

        public TextReader GetReader 
        {
            get { return _reader; }
        }

        public Dictionary<string, string> GetDictionary 
        {
            get { return _levelDictionary; }
        }
	}
}
