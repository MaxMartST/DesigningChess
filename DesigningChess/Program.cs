using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesigningChess
{
    class Program
    {
        public enum TypeFigure
        {
            Pawn,
            King,
            Queen,
            Elephant,
            Castle,
            Horse
        }

        abstract class Figure
        {
            public Figure(TypeFigure typeFigure, string nameFigure, (int x, int y) positionFigure)
            {
                if (nameFigure == null)
                {
                    throw new Exception("Не заданно имя фигуры");
                }

                Name = nameFigure;
                Position = positionFigure;

                switch (typeFigure)
                {
                    case TypeFigure.Pawn:
                        Type = "Пешка";
                        break;
                    case TypeFigure.King:
                        Type = "Король";
                        break;
                    case TypeFigure.Queen:
                        Type = "Королева";
                        break;
                    case TypeFigure.Elephant:
                        Type = "Слон";
                        break;
                    case TypeFigure.Castle:
                        Type = "Ладья";
                        break;
                    case TypeFigure.Horse:
                        Type = "Конь";
                        break;
                    default:
                        throw new FigureException("Тип не найден");
                }
            }
            public string Type { get; }
            public string Name { get; }
            public (int, int) Position { get; }
            public abstract double Step();
        }

        class Pawn : Figure
        {
            public Pawn(string nameFigure, (int x, int y) positionFigure) : base(TypeFigure.Pawn, nameFigure, positionFigure)
            { 
            }

            public override double Step()
            {
                throw new NotImplementedException();
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
                this.figures = new Figure[amountFigures];

                Random rnd = new Random();
                int numberFigure = 0;

                while (numberFigure < amountFigures)
                {
                    //int numberTypeFigure = Enum.GetNames(typeof(TypeFigure)).Length;
                    int numberTypeFigure = 0;

                    int x = rnd.Next(0, size);
                    int y = rnd.Next(0, size);
                    var position = (x, y);

                    if (this.field[x, y] == 1)
                    {
                        continue;
                    }

                    this.field[x, y] = 1;
                    this.figures[numberFigure] = GetRandomFigure(position, numberFigure, numberTypeFigure);

                    numberFigure++;
                }
            }

            static private Figure GetRandomFigure((int x, int y) positionFigure, int numberFigure, int numberTypeFigure)
            {
                switch (numberTypeFigure)
                {
                    case (int)TypeFigure.Pawn:
                        return new Pawn($"Пешка №{numberFigure}", positionFigure);
                    //case (int)TypeFigure.Rectangle:
                    //    sizeSide = GetRandomSize();
                    //    sizeSide1 = GetRandomSize();
                    //    return new Rectangle($"Прямоугольник №{numberFigure}", sizeSide, sizeSide1);
                    //case (int)TypeFigure.Triangle:
                    //    sizeSide = GetRandomSize();
                    //    sizeSide1 = GetRandomSize();
                    //    sizeSide2 = GetRandomSize();
                    //    return new Triangle($"Треугольник №{numberFigure}", sizeSide, sizeSide1, sizeSide2);
                    //case (int)TypeFigure.Circle:
                    //    sizeSide = GetRandomSize();
                    //    return new Circle($"Круг №{numberFigure}", sizeSide);
                    //case (int)TypeFigure.Cube:
                    //    sizeSide = GetRandomSize();
                    //    return new Cube($"Куб №{numberFigure}", sizeSide);
                    //case (int)TypeFigure.Ball:
                    //    sizeSide = GetRandomSize();
                    //    return new Ball($"Шар №{numberFigure}", sizeSide);
                    default:
                        throw new FigureException("Тип фигуры не найден");
                }
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
