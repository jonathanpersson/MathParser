using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using MathParserDemo.Controllers;

namespace MathParserDemo.Engine
{
    /// <summary>
    /// Handles parsing of input.
    /// </summary>
    static class Parser
    {
        /// <summary>
        /// Parse input
        /// </summary>
        /// <param name="input">Console input</param>
        /// <returns>If program is active</returns>
        public static bool Parse(string input)
        {
            input = TranslateCulture(input);
            List<string> lexedValues = Lexer.Split(input);

            if (lexedValues.Count <= 0) return true;

            // handle any exceptions thrown and display them as parse errors
            try
            {
                // check the keyword
                switch (lexedValues[0].ToLower())
                {
                    case "let":
                        Variables.Define(lexedValues[1], GetValue(lexedValues, 3));
                        break;
                    case "set":
                        Variables.Assign(lexedValues[1], GetValue(lexedValues, 3));
                        break;
                    case "calc":
                        Console.WriteLine(GetValue(lexedValues, 1));
                        break;
                    case "print":
                        Console.WriteLine(Controllers.Variables.GetValue(lexedValues[1]));
                        break;
                    case "rem":
                        Variables.Destroy(lexedValues[1]);
                        break;
                    case "exit":
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Parse error: {ex.Message}");
                Console.ResetColor();
            }


            return true;
        }

        /// <summary>
        /// Get value from expression
        /// </summary>
        /// <param name="line">The line</param>
        /// <param name="removeCount">How many items to remove from start of line</param>
        /// <returns>Value of parsed line</returns>
        private static double GetValue(List<string> line, int removeCount)
        {
            // remove the first x items
            line.RemoveRange(0, removeCount);

            // loop over operators
            foreach (char c in Globals.Operators)
            {
                // loop continuously until count is 1
                while (line.Count > 1)
                {
                    // break out of while-loop if there are no instances of c in line
                    if (!line.Contains(c.ToString())) break;

                    // loop over each item in line
                    for (int i = 0; i < line.Count; i++)
                    {
                        string item = line[i];

                        if (item == c.ToString())
                        {
                            string newItem = "";
                            double prev = Variables.Exists(line[i - 1]) ? Variables.GetValue(line[i - 1]) : double.Parse(line[i - 1]);
                            double next = Variables.Exists(line[i + 1]) ? Variables.GetValue(line[i + 1]) : double.Parse(line[i + 1]);

                            // do the math thing
                            switch (c)
                            {
                                case '^':
                                    newItem = (Math.Pow(prev, next)).ToString();
                                    break;
                                case '*':
                                    newItem = (prev * next).ToString();
                                    break;
                                case '/':
                                    newItem = (prev / next).ToString();
                                    break;
                                case '+':
                                    newItem = (prev + next).ToString();
                                    break;
                                case '-':
                                    newItem = (prev - next).ToString();
                                    break;
                            }

                            // insert new item and remove surrounding
                            line[i] = newItem;
                            line.RemoveAt(i + 1);
                            line.RemoveAt(i - 1);

                            // break out of for-loop
                            break;
                        }
                    }
                }
            }

            return double.Parse(line[0]);
        }

        /// <summary>
        /// Translate decimal separators between cultures.
        /// At least commas and dots.
        /// </summary>
        /// <param name="s">String to translate</param>
        /// <returns>Translated string</returns>
        private static string TranslateCulture(string s)
        {
            char currentCultureDecimalPoint = 
                char.Parse(Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);

            if (s.Contains('.') && currentCultureDecimalPoint != '.')
            {
                s = s.Replace('.', currentCultureDecimalPoint);
            }
            else if (s.Contains(',') && currentCultureDecimalPoint != ',')
            {
                s = s.Replace(',', currentCultureDecimalPoint);
            }

            return s;
        }
    }
}
