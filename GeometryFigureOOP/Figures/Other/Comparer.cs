using System.Collections.Generic;

namespace Figures
{
    class Comparer<T> : IComparer<T>
        where T : IFigureMath
    {
        public int Compare(T x, T y)
        {
            var X = x.GetSquar();
            var Y = y.GetSquar();

            return
                X < Y ? -1 :
                X > Y ? 1 :
                0;
        }
    }
}
