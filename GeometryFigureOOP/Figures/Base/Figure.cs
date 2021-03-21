using CustomGeometry;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Figures
{
    internal class Figure : IFigure , IFigureMath
    {
        protected readonly Geometric Geometric;
        protected readonly Path Path;

        protected Figure(Geometric geometric)
        {
            Geometric = geometric;
            Path = new Path()
            {
                Data = Geometric,
                Stretch = Stretch.Fill,
            };
        }

        public override string ToString()
        {
            return Geometric.ToString();
        }

        public static implicit operator Shape(Figure obj)
        {
            return obj.Path;
        }

        #region IFigure

        public virtual Brush Color
        {
            get => Path.Fill;
            set => Path.Fill = value;
        }

        public virtual double Width
        {
            get => Path.Width;
            set => Path.Width = value;
        }

        public virtual double Height
        {
            get => Path.Height;
            set => Path.Height = value;
        }

        #endregion

        #region IFigureMath

        public virtual double GetSquar()
        {
            throw new NotImplementedException($"Нет реализации расчета площади фигуру для - {Geometric.ToString()}");
        }

        #endregion

        #region Display

        public static void SetPosX(Figure figure, double X)
        {
            Canvas.SetLeft(figure.Path, X);
        }

        public static void SetPosY(Figure figure, double Y)
        {
            Canvas.SetTop(figure.Path, Y);
        }

        public static void SetPos(Figure figure, double X, double Y)
        {
            SetPosX(figure, X);
            SetPosY(figure, Y);
        }

        public static void ToolTipEnable(Figure figure)
        {
            figure.Path.ToolTip = new TextBlock()
            {
                Text = figure.Geometric.ToString()
            };
        }

        #endregion
    }
}
