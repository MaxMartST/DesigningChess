using System;

namespace DesigningChess
{
    class FigureException : Exception
    {
        public FigureException(string message)
        : base(message) { }
    }
}
