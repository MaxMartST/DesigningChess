using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesigningChess
{
    class Program
    {
        class Figure
        {
            public string name;

            public Figure(string name)
            {
                this.name = name;
            }
        }

        class Chess
        {
            private const int size = 8;
            int[,] field;
            Figure[] figures;

            public Chess(int amountFigures)
            {
                this.field = new int[size, size];

                Figure[] addFigures = new Figure[amountFigures];
                Random rnd = new Random();
                int count = 0;

                while (count < amountFigures)
                {
                    int x = rnd.Next(0, size);
                    int y = rnd.Next(0, size);
                    var position = (x, y);

                    if (this.field[x, y] == 1)
                    {
                        continue;
                    }

                    this.field[x, y] = 1;
                    addFigures[count] = GetRandomFigure(position, count);

                    count++;
                }

                this.figures = addFigures;
            }

            private Figure GetRandomFigure((int x, int y) positionFigure, int numberFigure)
            {
                return new Figure($"Фигура под номером: {numberFigure}");
            }

            public void PrintField()
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write($"{this.field[i, j]} \t");
                    }
                    Console.WriteLine();
                }
            }
        }

        static void Main(string[] args)
        {
            int amountFigures = 3;
            Chess chess = new Chess(amountFigures);

            chess.PrintField();

            Console.ReadLine();
        }
    }
}
