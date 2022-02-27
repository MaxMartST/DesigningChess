using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesigningChess
{
    class King : Figure
    {
        int[,] availableTravel;
        private const int size = 8;

        public King(string nameFigure, (int x, int y) positionFigure) :
            base(TypeFigure.King, nameFigure, positionFigure)
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
            int x, y;

            // Указываем позиции по вертикале
            x = positionFigure.x - 1;

            if (x >= 0)
            {
                this.availableTravel[x, positionFigure.y] = 1;
            }

            x = positionFigure.x + 1;

            if (x < size)
            {
                this.availableTravel[x, positionFigure.y] = 1;
            }

            // Указываем позиции по горизонтале
            y = positionFigure.y - 1;

            if (y >= 0)
            {
                this.availableTravel[positionFigure.x, y] = 1;
            }

            y = positionFigure.y + 1;

            if (y < size)
            {
                this.availableTravel[positionFigure.x, y] = 1;
            }

            // Вниз справа на лево
            x = positionFigure.x + 1;
            y = positionFigure.y - 1;

            if (x < size && y >= 0)
            {
                this.availableTravel[x, y] = 1;
            }

            // Вверх слева на право
            x = positionFigure.x - 1;
            y = positionFigure.y + 1;

            if (x > 0 && y < size)
            {
                this.availableTravel[x, y] = 1;
            }

            // Вверх справа на лево
            x = positionFigure.x - 1;
            y = positionFigure.y - 1;

            if (x >= 0 && y >= 0)
            {
                this.availableTravel[x, y] = 1;
            }

            // Вниз слева на право
            x = positionFigure.x + 1;
            y = positionFigure.y + 1;

            if (x < size && y < size)
            {
                this.availableTravel[x, y] = 1;
            }
        }
    }
}
