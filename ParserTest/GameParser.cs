﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameConcepts.GameParser
{
    public class GameParser
    {
        /// <summary>
        /// A list of characters (string-format).
        /// </summary>
        protected readonly List<string> _AsciiString = new List<string> ();

        /// <summary>
        /// A list of the level names
        /// </summary>
        private List<string> _namesOfLevels = new List<string> ();

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
        /// Placeholder variables for level name and level platform mappings
        /// </summary>
        private string _levelName;
        private List<string> _levelPlatforms;

        /// <summary>
        /// A placeholder value for the number of files in directory
        /// </summary>
        private int _numberOfLevels;

        /// <summary>
        /// The level to be used
        /// </summary>
        public int level;


        /// <summary>
        /// Retrives the files in the Levels-directory
        /// </summary>
        /// <returns>The files present text-files in the directory</returns>
        private string [] GetFiles ()
        {
            string [] dirContents = null;
            try {
                var currentDir = Directory.GetCurrentDirectory ();
                var levelsDir = Path.Combine (currentDir, "Levels");
                dirContents = Directory.GetFiles (levelsDir, "*.txt");
                return dirContents;
            } catch (UnauthorizedAccessException e) {
                Console.WriteLine (e.Message);
            }
            if (dirContents != null) {
                return dirContents;
            } else {
                Console.WriteLine ("There's nothing contained in the specified folder");
                return dirContents;
            }
        }

        /// <summary>
        /// Sets the number of levels
        /// </summary>
        public void SetListLevels ()
        {
            var files = GetFiles ();
            _numberOfLevels = files.Length;
        }

        /// <summary>
        /// Sets the list of level names.
        /// </summary>
        public void SetListLevelNames ()
        {
            Regex regx = new Regex (@"([^/]+(?=[\.]))");

            var files = GetFiles ();
            foreach (var file in files) {
                var name = regx.Match (file).ToString ();
                _namesOfLevels.Add (name);
            }
        }


        /// <summary>
        /// Calls all subsequent methods for retrieving level information with a TextReader object
        /// </summary>
        /// <param name="level">A TextReader object.</param>
        public void SetGameParser (int level)
        {
            var files = GetFiles ();
            using (var reader = File.OpenText (files [level])) {
                SetListLevels ();
                SetListLevelNames ();
                SetASCII (reader);
                SetProperties (reader);
                SetMappings (reader);
            }
        }

        /// <summary>
        /// Retrieves an ASCII-art image and returns this as a string-list. 
        /// </summary>
        /// <returns>A level to be translated</returns>
        /// <param name="reader">A TextReader object</param>
        private void SetASCII (TextReader reader)
        {
            try {
                for (int i = 0; i < (_levelWidth * _levelHeight) + _levelHeight; i++) {
                    int integer = reader.Read ();

                    if (integer == -1)
                        break;

                    char character = (char)integer;

                    if ((character != '\n') && (character != '\r')) {
                        _AsciiString.Add (character.ToString ());
                    }
                }
            } catch (IndexOutOfRangeException e) {
                string msg = e.Message;
                Console.WriteLine ("{0} Either the text-file does not contain a level - otherwise the level is incorrectly formatted", msg);
            }
        }


        /// <summary>
        /// Retrieves the level's properties and returns the level-name and platform mappings in seperate variables
        /// </summary>
        /// <param name="reader"> A TextReader object</param>
        private void SetProperties (TextReader reader)
        {
            string nameEval = "Name:";
            string platformEval = "Platforms:";

            string leftPattern = @"(?<=[";
            string rightPattern = @"])\s.*$";

            Regex nameRgx = new Regex (leftPattern + nameEval + rightPattern);
            Regex platformRgx = new Regex (leftPattern + platformEval + rightPattern);

            string line;

            for (int i = 0; i < 4; i++) {
                line = reader.ReadLine ();
                if (line == "") {
                    continue;
                } else if (line.Contains (nameEval) == true) {
                    _levelName = nameRgx.Match (line).ToString ().Replace (" ", "");

                } else if (line.Contains (platformEval) == true) {
                    _levelPlatforms = platformRgx.Match (line).ToString ().Replace (" ", "").Split (',').ToList ();
                }
            }
        }

        /// <summary>
        /// A method for parsing the level mappings of ASCII-characters
        /// </summary>
        /// <param name="reader">A TextReader object</param>
        private void SetMappings (TextReader reader)
        {
            string identifierPattern = @"(^.)";
            Regex identifierRgx = new Regex (identifierPattern);

            string pathPattern = @"((?<=[)])\s).*$";
            Regex pathRgx = new Regex (pathPattern);

            string line;

            while ((line = reader.ReadLine ()) != null) {
                if (line == "") {
                    continue;
                }
                string identifier = identifierRgx.Match (line).ToString ();
                string path = pathRgx.Match (line).ToString ();
                _levelDictionary.Add (identifier, path);
            }
        }

        /// <summary>
        /// Gets the string list.
        /// </summary>
        /// <value>The string list.</value>
        public List<string> GetAsciiString {
            get { return _AsciiString; }
        }

        /// <summary>
        /// Gets the get levelname.
        /// </summary>
        /// <value> Level Name.</value>
        public string GetLevelname {
            get { return _levelName; }
        }

        /// <summary>
        /// Gets the get level platforms.
        /// </summary>
        /// <value>Level platform mappings.</value>
        public List<string> GetLevelPlatforms {
            get { return _levelPlatforms; }
        }

        /// <summary>
        /// Gets the get dictionary file.
        /// </summary>
        /// <value> Level mappings </value>
        public Dictionary<string, string> GetDictionaryFile {
            get { return _levelDictionary; }
        }

        /// <summary>
        /// Get the number of levels available
        /// </summary>
        /// <value> Available levels </value>
        public int GetNumberOfLevels {
            get { return _numberOfLevels; }
        }

        public List<string> GetNamesOfLevels {
            get { return _namesOfLevels; }
        }
    }
}

