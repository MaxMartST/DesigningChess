using System;

namespace DesigningChess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество фигур на шахматной доске: ");
            int amountFigures = Convert.ToInt32(Console.ReadLine());

            Chess chess = new Chess(amountFigures);
            chess.MakeMove();

            Console.ReadLine();
        }
    }
}
