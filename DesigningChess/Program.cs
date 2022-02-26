using System;

namespace DesigningChess
{
    class Program
    {
        static void Main(string[] args)
        {
            int amountFigures = 3;
            Chess chess = new Chess(amountFigures);
            chess.MakeMove();

            Console.ReadLine();
        }
    }
}
