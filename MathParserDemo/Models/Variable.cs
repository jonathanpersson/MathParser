using System;
using System.Collections.Generic;
using System.Text;

namespace MathParserDemo.Models
{
    /// <summary>
    /// Variable model
    /// </summary>
    class Variable
    {
        /// <summary>
        /// Variable identifier
        /// </summary>
        private string _identifier;

        /// <summary>
        /// Variable value
        /// </summary>
        private int _value;

        /// <summary>
        /// Variable identifier
        /// </summary>
        public string Identifier => _identifier;

        /// <summary>
        /// Variable value
        /// </summary>
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Construct a new variable
        /// </summary>
        /// <param name="identifier">Variable identifier</param>
        /// <param name="value">Variable value</param>
        public Variable(string identifier, int value)
        {
            _identifier = identifier;
            _value = value;
        }
    }
}
