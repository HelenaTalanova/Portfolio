using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Barley_Break
{
    class Cube
    {
        #region Const

        /// <summary>
        /// Толщина сетки
        /// </summary>
        public static readonly int MeshThickness = 2;

        /// <summary>
        /// Кисть кубика
        /// </summary>
        private static readonly Brush BrushCube = Brushes.DarkBlue;

        /// <summary>
        /// Кисть для рамки кубика с активным фокусом
        /// </summary>
        private static readonly Brush BorderIsFocusColor = Brushes.LightGreen;

        /// <summary>
        /// Кисть для рамки кубика с не атикным фокусом
        /// </summary>
        private static readonly Brush BorderNoFocusColor = Brushes.Transparent;

        /// <summary>
        /// Кисть для текста кубика не на своей позиции
        /// </summary>
        private static readonly Brush ForegroundTextNoPos = Brushes.White;

        /// <summary>
        /// Кисть для текста кубика на своей позиции
        /// </summary>
        private static readonly Brush ForegroundTextIsPos = Brushes.Orange;

        #endregion

        #region Property

        /// <summary>
        /// Ширина контейнера кубика
        /// </summary>
        public static double Width = 80;

        /// <summary>
        /// Высота контейнера кубика
        /// </summary>
        public static double Height = 70;

        /// <summary>
        /// Максимальное количество элементов в строке
        /// </summary>
        public static int MaxCountLine { get; set; }

        /// <summary>
        /// Номер кубика
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// Визуальный нтерфейс
        /// </summary>
        public Grid Content { get; }

        /// <summary>
        /// Свойство задающее состояние фокуса кубика
        /// </summary>
        public bool Focused
        {
            set
            {
                Content.Background =
                    value ?
                    BorderIsFocusColor :
                    BorderNoFocusColor;
            }
        }

        /// <summary>
        /// Позиция по "X"
        /// </summary>
        public int X
        {
            get => x;
            set
            {
                x = value;
                var pos_x = x * Width + x * MeshThickness;
                Canvas.SetLeft(Content, pos_x);
                UpDatePos();
            }
        }
        private int x;

        /// <summary>
        /// Позиция по "Y"
        /// </summary>
        public int Y
        {
            get => y;
            set
            {
                y = value;
                var pos_y = y * Height + y * MeshThickness;
                Canvas.SetTop(Content, pos_y);
                UpDatePos();
            }
        }
        private int y;

        #endregion

        public Cube(int num, int x, int y)
        {
            Num = num;
            cube = CreateCube();
            text = CreateText(num);
            Content = CreateContent(cube, text);
            X = x;
            Y = y;
        }

        public event EventHandler GotFocus;
        public event EventHandler LostFocus;
        public event EventHandler Click;

        public delegate void EventHandler(object sender, EventArgs e);

        #region Private

        private Rectangle cube { get; }
        private TextBlock text { get; }

        /// <summary>
        /// Обновление текста при смене позиции
        /// </summary>
        private void UpDatePos()
        {
            if (Num == ((y) * MaxCountLine) + (x + 1))
            {
                text.Foreground = ForegroundTextIsPos;
            }
            else
            {
                text.Foreground = ForegroundTextNoPos;
            }
        }

        /// <summary>
        /// Создание кубика
        /// </summary>
        /// <returns></returns>
        private Rectangle CreateCube()
        {
            return new Rectangle
            {
                Fill = BrushCube,
                Margin = new Thickness(1),
                RadiusX = 15,
                RadiusY = 15,

            };
        }

        /// <summary>
        /// Создание текта кубика
        /// </summary>
        /// <param name="content">Отображаемый номер</param>
        /// <returns></returns>
        private TextBlock CreateText(int content)
        {
            var text = new TextBlock
            {
                Text = content.ToString(),
                FontSize = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            return text;
        }

        /// <summary>
        /// Создание визуального контента
        /// </summary>
        /// <typeparam name="T">Визуальный элемент/typeparam>
        /// <typeparam name="V">визуальный элемент</typeparam>
        /// <param name="cube">Кубик</param>
        /// <param name="text">Текст кубика</param>
        /// <returns></returns>
        private Grid CreateContent<T, V>(T cube, V text)
            where T : UIElement
            where V : UIElement
        {
            var content = new Grid
            {
                Height = Height,
                Width = Width
            };

            content.Children.Add(cube);
            content.Children.Add(text);

            content.MouseEnter += (s, e) =>
            {
                if (s is Grid rec)
                {
                    var handler = GotFocus;
                    handler?.Invoke(this, new EventArgs());
                }
            };

            content.MouseLeave += (s, e) =>
            {
                if (s is Grid rec)
                {
                    var handler = LostFocus;
                    handler?.Invoke(this, new EventArgs());
                }
            };

            content.MouseLeftButtonUp += (s, e) =>
            {
                if (s is Grid rec)
                {
                    var handler = Click;
                    handler?.Invoke(this, new EventArgs());
                }
            };

            return content;
        }

        #endregion
    }
}
