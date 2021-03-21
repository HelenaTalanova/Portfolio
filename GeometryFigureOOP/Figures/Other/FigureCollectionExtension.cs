using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Figures
{
    internal static class FigureCollectionExtension
    {
        public static IEnumerable<Figure> ShowIn(this IEnumerable<Figure> E, Panel Filed)
        {
            foreach (var figure in E)
                Filed.Children.Add(figure);

            return E;
        }

        public static IEnumerable<Figure> WrapIn(this IEnumerable<Figure> E, double WidthField, double Margin)
        {
            double X = Margin;
            double Y = Margin;
            double MaxHeight = 0;

            foreach (var figure in E)
            {
                if (X + figure.Width >= WidthField)
                {
                    X = Margin;
                    Y += Margin + MaxHeight;
                    MaxHeight = 0;
                }

                Figure.SetPos(figure, X, Y);

                X += Margin + figure.Width;

                if (MaxHeight < figure.Height)
                    MaxHeight = figure.Height;
            }

            return E;
        }

        public static IEnumerable<Figure> SortToSize(this IEnumerable<Figure> E)
        {
            var list = E.ToList();

            list.Sort(new Comparer<IFigureMath>());

            return list;
        }
    }
}
