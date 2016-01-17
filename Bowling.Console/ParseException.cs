using System;

namespace Bowling.Console
{
    public class ParseException : Exception
    {
        public ParseException(string message) : base(message)
        {
        }
    }
}