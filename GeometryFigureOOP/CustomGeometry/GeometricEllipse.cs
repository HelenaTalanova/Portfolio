using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CustomGeometry
{
    class GeometricEllipse : Geometric
    {
        public GeometricEllipse()
        {
            FigureType = GeometricFigures.Ellipse;
            Geometry = new EllipseGeometry(new Rect(0, 0, 100, 100));
        }
    }
}
