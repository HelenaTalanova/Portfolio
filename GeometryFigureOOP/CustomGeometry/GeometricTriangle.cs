using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CustomGeometry
{
    class GeometricTriangle : Geometric
    {
        public GeometricTriangle()
        {
            FigureType = GeometricFigures.Triangle;

            var pathFigure = new PathFigure
            {
                StartPoint = new Point(0, 100),
                IsClosed = true,
            };

            pathFigure.Segments.Add(new LineSegment(new Point(100 / 2, 0), false));
            pathFigure.Segments.Add(new LineSegment(new Point(100, 100), false));

            var figureCollection = new PathFigureCollection();

            figureCollection.Add(pathFigure);

            Geometry = new PathGeometry(figureCollection);
        }
    }
}
