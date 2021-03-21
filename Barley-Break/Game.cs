using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Barley_Break
{
    static class Game
    {
        static bool[,] cubes;

        /// <summary>
        /// Запуск игры
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="container"></param>
        /// <param name="CountX"></param>
        /// <param name="CountY"></param>
        static public void Start<T>(T container, int CountX, int CountY)
            where T : Panel
        {
            container.Children.Clear();

            cubes = new bool[CountX, CountY];
            Cube.MaxCountLine = CountX;

            Cube.Width = 500 / CountX - Cube.MeshThickness;
            Cube.Height = 500 / CountY - Cube.MeshThickness;

            var x = 0;
            var y = 0;

            foreach (var num in GetRandomNumber((CountX * CountY) - 1))
            {
                cubes[x, y] = true;
                var cube = new Cube(num, x, y);
                cube.GotFocus += EventGotFocus;
                cube.LostFocus += EventLostFocus;
                cube.Click += EventClick;
                container.Children.Add(cube.Content);

                if (++x == cubes.GetLength(0))
                {
                    y++;
                    x = 0;
                }
            }
        }

        public static event Action Step;

        /// <summary>
        /// Обработчик потери фокуса кубиком
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void EventLostFocus(object sender, EventArgs e)
        {
            if (sender is Cube cube)
            {
                cube.Focused = false;
            }
        }

        /// <summary>
        /// Обработчик получения фокуса кубиком
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void EventGotFocus(object sender, EventArgs e)
        {
            if (sender is Cube cube)
            {
                cube.Focused = true;
            }
        }

        /// <summary>
        /// Проверка свободного места для возможности перемещения в заданную ячейку
        /// </summary>
        /// <param name="x">Позиция по "X"</param>
        /// <param name="y">Позиция по "Y"</param>
        /// <returns></returns>
        private static bool IsFree(int x, int y)
        {
            if ((x >= 0) &&
                (y >= 0) &&
                (x < cubes.GetLength(0)) &&
                (y < cubes.GetLength(1)))
            {
                return cubes[x, y] == false;
            }

            return false;
        }

        /// <summary>
        /// Обработчик клика на кубик, перемещение кубика
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void EventClick(object sender, EventArgs e)
        {
            if (sender is Cube cube)
            {
                var x = cube.X;
                var y = cube.Y;
                var step = false;

                if (IsFree(cube.X + 1, cube.Y))
                {
                    x++;
                    step = true;
                }
                else if (IsFree(cube.X - 1, cube.Y))
                {
                    x--;
                    step = true;
                }
                else if (IsFree(cube.X, cube.Y + 1))
                {
                    y++;
                    step = true;
                }
                else if (IsFree(cube.X, cube.Y - 1))
                {
                    y--;
                    step = true;
                }

                cubes[cube.X, cube.Y] = false;
                cubes[x, y] = true;
                cube.X = x;
                cube.Y = y;

                if (step)
                    Step.Invoke();
            }
        }

        /// <summary>
        /// Получение случайных номеров для кубиков
        /// </summary>
        /// <param name="max">Количество кубиков</param>
        /// <returns></returns>
        private static IEnumerable<int> GetRandomNumber(int max)
        {
            var random = new Random();
            var nums = new List<int>();

            for (int i = 1; i < max + 1; i++)
            {
                nums.Add(i);
            }

            for (int i = 0; i < max; i++)
            {
                var index = random.Next(0, nums.Count - 1);
                var num = nums[index];
                nums.RemoveAt(index);

                yield return num;
            }
        }
    }
}
