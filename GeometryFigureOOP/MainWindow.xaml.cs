using Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GeometryDemo
{
    public partial class MainWindow : Window
    {
        List<Figure> Figures;

        public MainWindow()
        {
            InitializeComponent();

            /// Поле для фигур
            var field = GetFigureField(this);
            this.SizeChanged += (o, e) => Figures.WrapIn(field.ActualWidth, 5);

            Sort.Click += (o, e) => Figures = Figures.SortToSize().WrapIn(field.ActualWidth, 5).ToList();

            /// Перечисление случайных фигур
            Figures = GetRandomFigures(100).ToList();

            /// Вывести на экран
            Figures.ShowIn(field);

            /// Вывести площадь всех фигур
            Console.Text = "Фигура - Площадь фигуры\n\n";
            foreach (var e in Figures.SortToSize())
            {
                Console.Text += string.Format("{0,-15} - {1,6:N0}\n", e, e.GetSquar());
            }
        }

        static Panel GetFigureField(Window window)
        {
            if (window.Content is Panel panel)
                foreach (var children in panel.Children)
                    if (children is Panel field)
                        return field;
            throw new Exception("Не найдено поле с графическими элементами");
        }

        #region Create Demo

        static IEnumerable<Figure> GetRandomFigures(int count)
        {
            const int MinWidth = 10;
            const int MaxWidth = 50;
            const int MinHeight = 10;
            const int MaxHeight = 50;

            var random = new Random();

            while (0 < count--)
            {
                var W = random.Next(MinWidth, MaxWidth);
                var H = random.Next(MinHeight, MaxHeight);

                Figure figure = null;

                switch (random.Next(0, 5))
                {
                    case 0:
                        figure = new FigureQuadre(W, RandomColor(random));
                        break;
                    case 1:
                        figure = new FigureRectangle(W, H, RandomColor(random));
                        break;
                    case 2:
                        figure = new FigureCircle(W, RandomColor(random));
                        break;
                    case 3:
                        figure = new FigureEllipse(W, H, RandomColor(random));
                        break;
                    case 4:
                        figure = new FigureTriangle(W, H, RandomColor(random));
                        break;
                    default:
                        throw new Exception("Не удалось создать игуру");
                }

                Figure.ToolTipEnable(figure);

                yield return figure;
            }
        }

        static Brush RandomColor(Random random)
        {
            switch (random.Next(0, 10))
            {
                case 0: return Brushes.Gray;
                case 1: return Brushes.Red;
                case 2: return Brushes.Green;
                case 3: return Brushes.Blue;
                case 4: return Brushes.Brown;
                case 5: return Brushes.Black;
                case 6: return Brushes.Orange;
                case 7: return Brushes.Yellow;
                case 8: return Brushes.LightBlue;
                case 9: return Brushes.LightGray;
            }
            return Brushes.Transparent;
        }

        #endregion
    }
}