using System.Windows;
using System.Windows.Media;

namespace CustomGeometry
{
    class GeometricCircle : Geometric
    {
        public GeometricCircle()
        {
            FigureType = GeometricFigures.Circle;
            Geometry = new EllipseGeometry(new Rect(0, 0, 100, 100));
        }
    }
}
