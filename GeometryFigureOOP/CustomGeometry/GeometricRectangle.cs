using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CustomGeometry
{
    class GeometricRectangle : Geometric
    {
        public GeometricRectangle()
        {
            FigureType = GeometricFigures.Rectangle;
            Geometry = new RectangleGeometry(new Rect(0, 0, 100, 100));
        }
    }
}
