using System.Windows.Media;

namespace CustomGeometry
{
    abstract class Geometric
    {
        public GeometricFigures FigureType { get; protected set; }
        public Geometry Geometry { get; protected set; }

        public override string ToString()
        {
            return FigureType.ToText();
        }

        public static implicit operator Geometry(Geometric obj)
        {
            return obj.Geometry;
        }
    }
}
