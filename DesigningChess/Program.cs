using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
