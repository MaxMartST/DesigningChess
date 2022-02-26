using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesigningChess
{
    class Queen : Figure
    {
        int[,] availableTravel;
        private const int size = 8;

        public Queen(string nameFigure, (int x, int y) positionFigure) :
            base(TypeFigure.Queen, nameFigure, positionFigure)
        {
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
            this.availableTravel = new int[size, size];
            this.availableTravel[positionFigure.x, positionFigure.y] = 1;

            // Отмечаем позиции, куда может сделать ход фигура
            int x = positionFigure.x;
            int y = positionFigure.y;

            // По оси X и по оси Y
            for (y = 0; y < size; y++)
            {
                this.availableTravel[positionFigure.x, y] = 1;
            }

            for (x = 0; x < size; x++)
            {
                this.availableTravel[x, positionFigure.y] = 1;
            }

            // Вниз справа на лево
            x = positionFigure.x;
            y = positionFigure.y;

            while (x < size && y >= 0)
            {
                this.availableTravel[x, y] = 1;
                x++;
                y--;
            }

            // Вверх слева на право
            x = positionFigure.x;
            y = positionFigure.y;

            while (x > 0 && y < size)
            {
                this.availableTravel[x, y] = 1;
                x--;
                y++;
            }

            // Вверх справа на лево
            x = positionFigure.x;
            y = positionFigure.y;

            while (x >= 0 && y >= 0)
            {
                this.availableTravel[x, y] = 1;
                x--;
                y--;
            }

            // Вниз слева на право
            x = positionFigure.x;
            y = positionFigure.y;

            while (x < size && y < size)
            {
                this.availableTravel[x, y] = 1;
                x++;
                y++;
            }
        }
    }
}
