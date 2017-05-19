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
		protected readonly List<string> _levelString = new List<string>();

        /// <summary>
        /// A dictionary containing key/value-pairs for ASCII-character to Path to image. 
        /// </summary>
        protected readonly Dictionary<string, string> _levelDictionary = new Dictionary<string, string> ();

		/// <summary>
		/// The predefined dimensions of the ASCII-art image
		/// </summary>
		private int _levelHeight = 23;
		private int _levelWidth = 40;

        private string _levelName;
        private string _levelPlatforms;

        /// <summary>
        /// The level to be used
        /// </summary>
        public int _level;

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
        public void ListLevels ()
        {
            var files = GetFiles ();
            Console.WriteLine ("Choose a level:");
            for (int i = 0; i < files.Length; i++) {

                Console.Write (i + ")");
                Console.Write (files [i]);
                Console.WriteLine ("");
            }
        }



        public void GetLevel (int level)
        {
            var files = GetFiles ();
            using (var reader = File.OpenText (files [level])) 
            {
                GetASCII (reader);
                GetProperties (reader);
                GetMappings (reader);
            }
            
        }

		/// <summary>
		/// Retrieves an ASCII-art image and returns this as a string-list. 
		/// </summary>
		/// <returns>A level to be translated</returns>
		/// <param name="reader">A TextReader object</param>

		public void GetASCII(TextReader reader)
		{
			try
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
                        _levelString.Add(letter);
                    }
				}
			}
			catch (IndexOutOfRangeException e)
			{
				string msg = e.Message;
				Console.WriteLine("{0} Either the text-file does not contain a level - otherwise the level is incorrectly formatted", msg);
			}
		}


        /// <summary>
        /// Retrieves the level's properties and returns the level-name and platform mappings in seperate variables
        /// </summary>
        /// <param name="reader"> A TextReader object</param>
        public void GetProperties (TextReader reader)
        {
            string nameEval = "Name:";
            string platformEval = "Platforms:";

            string leftPattern = @"(?<=[";
            string rightPattern = @"])\s.*$";

            Regex nameRgx = new Regex (leftPattern + nameEval + rightPattern);
            Regex platformRgx = new Regex (leftPattern + platformEval + rightPattern);

            string line;

            for (int i = 0; i < 4; i++) 
            {
                line = reader.ReadLine ();
                if (line == "") 
                {
                    continue;
                }
                else if(line.Contains (nameEval) == true) 
                {
                    _levelName = nameRgx.Match (line).ToString ().Replace(" ", "");

                } 
                else if (line.Contains (platformEval) == true) 
                {
                    _levelPlatforms = platformRgx.Match (line).ToString ().Replace (" ", "");
                    //_levelPlatforms = Int32.Parse (plt);
                } 

            }

        }

		public void GetMappings(TextReader reader)
		{
			string identifierPattern = @"(^.)";
			Regex identifierRgx = new Regex(identifierPattern);

			string pathPattern = @"((?<=[)])\s).*$";
			Regex pathRgx = new Regex(pathPattern);

			string line;

			while ((line = reader.ReadLine()) != null)
			{
				if (line == "")
				{
                    continue;
				}
				string identifier = identifierRgx.Match(line).ToString();
				string path = pathRgx.Match(line).ToString();
				_levelDictionary.Add(identifier, path);
			}
		}



        /// <summary>
        /// Gets the string list.
        /// </summary>
        /// <value>The string list.</value>
		public List<string> GetLevelString
		{
			get { return _levelString; }
		}

        public string GetLevelname 
        {
            get { return _levelName; }
        }

        public string GetLevelPlatforms 
        {
            get { return _levelPlatforms; }
        }

        public Dictionary<string, string> GetDictionaryFile 
        {
            get { return _levelDictionary; }
        }
	}
}
