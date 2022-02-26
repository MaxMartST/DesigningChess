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
            public (int x, int y) Position { get; set; }
            public abstract void Step((int x, int y) newPositionFigure, int[,] field);
            public abstract void SetAvailableTravelAndPosition((int x, int y) positionFigure);
        }

        class Pawn : Figure
        {
            int[,] availableTravel;
            private const int size = 8;

            public Pawn(string nameFigure, (int x, int y) positionFigure) : base(TypeFigure.Pawn, nameFigure, positionFigure)
            {
                this.availableTravel = new int[size, size];

                SetAvailableTravelAndPosition(positionFigure);
            }

            public override void Step((int x, int y) newPositionFigure, int[,] field)
            {
                if (this.Position.x == newPositionFigure.x && this.Position.y == newPositionFigure.y)
                {
                    throw new FigureException($"Фигура под именем: {this.Name}, не может перейти на поле, на котором она уже стоит.");
                }

                if (field[newPositionFigure.x, newPositionFigure.y] == 1)
                {
                    throw new FigureException($"Фигура под именем: {this.Name}, не может перейти на поле, на котором она уже стоит другая фигура.");
                }

                if (availableTravel[newPositionFigure.x, newPositionFigure.y] != 1)
                {
                    throw new FigureException($"Новые координаты не соответствуют правилам хода для фигуры с типом {this.Type}.");
                }

                // Задать новую позицию фигуры и допустимые ходы
                SetAvailableTravelAndPosition(newPositionFigure);

                Position = newPositionFigure;
            }

            public override void SetAvailableTravelAndPosition((int x, int y) positionFigure)
            {
                this.availableTravel[positionFigure.x, positionFigure.y] = 1;

                int x = positionFigure.x + 1;

                if (x < size)
                {
                    this.availableTravel[x, positionFigure.y] = 1;
                }

                x = positionFigure.x - 1;

                if (x >= 0)
                {
                    this.availableTravel[x, positionFigure.y] = 1;
                }
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

                // насальная позиция
                //int x = 0;
                //int y = 0;

                while (numberFigure < amountFigures)
                {
                    // задвем случайный тип фигуры
                    //int maxTypeFigure = Enum.GetNames(typeof(TypeFigure)).Length;
                    //var numberTypeFigure = rnd.Next(0, maxTypeFigure);

                    int numberTypeFigure = 0;

                    // задаем случайные позиции фигуры, с учётом занятого поля
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

                    // следующая позиция
                    //x++;
                    //y++;
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
                        //Console.Write($"{this.field[i, j]} \t");
                        if (this.field[i, j] == 0)
                        {
                            Console.Write($"{this.field[i, j]}\t\t");
                            continue;
                        }
                        else
                        {
                            var figure = this.figures.First(f => f.Position == (i, j));
                            Console.Write($"{figure.Name} \t");
                        } 
                    }
                    Console.WriteLine();
                }
            }

            public void MakeMove()
            {
                while (true)
                {
                    PrintField();

                    try
                    {
                        Console.WriteLine("Введите имя фигуры и нажмите Enter.\nИли для выхода просто нажмите Enter...");
                        Console.Write("Имя фмгуры: ");
                        string input = Console.ReadLine();

                        if (String.IsNullOrEmpty(input))
                        {
                            Console.WriteLine("Выход из игры");
                            break;
                        }

                        // Находим индекс интересующей нас фигуры
                        var indexFigure = GetIndexFigure(input);

                        Console.WriteLine($"Введите координаты для фигуры {input} по X и Y.");
                        (int x, int y) position;

                        Console.Write("X : ");
                        position.x = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Y : ");
                        position.y = Convert.ToInt32(Console.ReadLine());

                        if (position.x > size || position.x < 0 ||
                            position.y > size || position.y < 0)
                        {
                            throw new FigureException($"Координаты выходят за приделы поля:\nПо оси X: от 0 до {size - 1}\nПо оси Y: от 0 до {size - 1}");
                        }

                        // Сохраняем старую позицию
                        var tempPosition = this.figures[indexFigure].Position;

                        // Делаем ход фигурой и отрабатываем исключения в случае чего
                        this.figures[indexFigure].Step(position, this.field);
                        
                        // Затераем старую позицию
                        this.field[tempPosition.x, tempPosition.y] = 0;

                        // Отмечаем новую
                        this.field[position.x, position.y] = 1;
                    }
                    catch (FigureException fe)
                    {
                        Console.WriteLine($"Ошибка: {fe.Message}.");
                    }
                }
            }

            private int GetIndexFigure(string numaeFigure)
            {
                for (int i = 0; i < this.figures.Length; i++)
                {
                    if (this.figures[i].Name == numaeFigure)
                    {
                        return i;
                    }
                }

                throw new FigureException($"Фигура с именем {numaeFigure} ненайдена");
            }
        }

        static void Main(string[] args)
        {
            int amountFigures = 3;
            Chess chess = new Chess(amountFigures);

            chess.MakeMove();

            Console.ReadLine();
        }
    }
}
