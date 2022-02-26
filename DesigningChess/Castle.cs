using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesigningChess
{
    class Castle : Figure
    {
        int[,] availableTravel;
        private const int size = 8;

        public Castle(string nameFigure, (int x, int y) positionFigure) : 
            base(TypeFigure.Castle, nameFigure, positionFigure)
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
            for (int y = 0; y < size; y++)
            {
                this.availableTravel[positionFigure.x, y] = 1;
            }

            for (int x = 0; x < size; x++)
            {
                this.availableTravel[x, positionFigure.y] = 1;
            }
        }
    }
}
