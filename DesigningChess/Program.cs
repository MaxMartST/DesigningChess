using System;

namespace DesigningChess
{
    class Program
    {
        static void Main(string[] args)
        {
            int amountFigures = 0;

            do 
            {
                Console.Write("Введите количество фигур на шахматной доске: ");
                amountFigures = Convert.ToInt32(Console.ReadLine());

                if (amountFigures > 64)
                {
                    Console.WriteLine("Количество фигур превышает допустимое количество в размере 64");
                }
            } 
            while (amountFigures > 64);
            

            Chess chess = new Chess(amountFigures);
            chess.MakeMove();

            Console.ReadLine();
        }
    }
}
