using System;
using System.Collections.Generic;
using MathParserDemo.Models;

namespace MathParserDemo.Controllers
{
    static class Variables
    {
        /// <summary>
        /// Currently defined variables
        /// </summary>
        private static Dictionary<string, Variable> Memory = new Dictionary<string, Variable>();

        /// <summary>
        /// Define a new variable
        /// </summary>
        /// <param name="identifier">Variable identifier</param>
        /// <param name="value">Variable value</param>
        public static void Define(string identifier, double value)
        {
            if (!Exists(identifier))
            {
                if (ValidateIdentifier(identifier))
                {
                    Variable newVariable = new Variable(identifier, value);
                    Memory.Add(identifier, newVariable);
                }
                else throw new Exception($"\"{identifier}\" is not a valid variable name. Identifier cannot start with a number.");
            }
            else throw new Exception("Cannot redefine variable that already exists.");
        }

        /// <summary>
        /// Remove a variable
        /// </summary>
        /// <param name="identifier">Variable to destroy</param>
        public static void Destroy(string identifier)
        {
            if (Exists(identifier)) Memory.Remove(identifier);
            else throw new Exception("Cannot remove undefined variable.");
        }

        /// <summary>
        /// Assign (set) a new value for a variable
        /// </summary>
        /// <param name="identifier">Variable to assign to</param>
        /// <param name="value">New value</param>
        public static void Assign(string identifier, double value)
        {
            if (Exists(identifier)) Memory[identifier].Value = value;
            else throw new Exception("Cannot set undefined variable.");
        }

        /// <summary>
        /// Check if a variable is already defined
        /// </summary>
        /// <param name="identifier">Identifier to look for</param>
        /// <returns>True if variable is already defined</returns>
        public static bool Exists(string identifier)
        {
            return Memory.ContainsKey(identifier);
        }

        /// <summary>
        /// Get variable value
        /// </summary>
        /// <param name="identifier">Variable to get</param>
        /// <returns>Variable value, if it exsits</returns>
        public static double GetValue(string identifier)
        {
            if (Exists(identifier)) return Memory[identifier].Value;
            throw new Exception("Cannot get value of a variable that is not defined.");
        }

        /// <summary>
        /// Validate identifier
        /// </summary>
        /// <returns>True if identifier is valid</returns>
        public static bool ValidateIdentifier(string identifier)
        {
            return !int.TryParse(identifier[0].ToString(), out int j);
        }
    }
}
