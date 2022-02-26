using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesigningChess
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
}
