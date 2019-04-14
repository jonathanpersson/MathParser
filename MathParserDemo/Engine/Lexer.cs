using System;
using System.Collections.Generic;

namespace MathParserDemo.Engine
{
    /// <summary>
    /// A lexical analyzer. Handles converting input strings to words that can be parsed later.
    /// </summary>
    static class Lexer
    {
        /// <summary>
        /// Split a string into words based on string content
        /// </summary>
        /// <param name="line">The line to split</param>
        /// <returns>List of words in line</returns>
        public static List<string> Split(string line)
        {
            List<string> lexedString = new List<string>();
            string currentWord = "";

            foreach (char c in line)
            {
                if (IsBlankspace(c))
                {
                    if (currentWord.Length > 0)
                    {
                        lexedString.Add(currentWord);
                        currentWord = "";
                    }

                    // does nothing if currentWord is blank
                }
                else if (IsOperator(c) || c == Globals.Equals)
                {
                    if (currentWord.Length > 0)
                    {
                        lexedString.Add(currentWord);
                        currentWord = "";
                    }

                    lexedString.Add(c.ToString());
                }
                else
                {
                    currentWord += c;
                }
            }

            if (currentWord.Length > 0) lexedString.Add(currentWord);

            return lexedString;
        }

        /// <summary>
        /// Check if a character is a type of blankspace
        /// </summary>
        /// <param name="c">Character to check</param>
        /// <returns>True if character is a blankspace</returns>
        private static bool IsBlankspace(char c)
        {
            return Globals.Blankspaces.Contains(c);
        }

        /// <summary>
        /// Check if a character is a mathematical operator
        /// </summary>
        /// <param name="c">Character to check</param>
        /// <returns>True if character is an operator</returns>
        private static bool IsOperator(char c)
        {
            return Globals.Operators.Contains(c);
        }
    }
}
