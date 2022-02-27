using System;
using System.Linq;

namespace DesigningChess
{
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

                int numberTypeFigure = 1;

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
                case (int)TypeFigure.Castle:
                    return new Castle($"Ладья №{numberFigure}", positionFigure);
                case (int)TypeFigure.Elephant:
                    return new Elephant($"Слон №{numberFigure}", positionFigure);
                case (int)TypeFigure.Queen:
                    return new Queen($"Королева №{numberFigure}", positionFigure);
                case (int)TypeFigure.Horse:
                    return new Horse($"Конь №{numberFigure}", positionFigure);
                case (int)TypeFigure.King:
                    return new King($"Король №{numberFigure}", positionFigure);
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

                    Console.WriteLine($"Для перемещения, введите координаты для фигуры {input} по X и Y");
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
}
