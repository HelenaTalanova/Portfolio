using CustomGeometry;
using System.Windows.Media;

namespace Figures
{
    internal class FigureEllipse : Figure
    {
        public FigureEllipse()
            : base(new GeometricEllipse()) { }

        public FigureEllipse(double RadiusX, double RadiusY)
            : this()
        {
            Width = RadiusX;
            Height = RadiusY;
        }

        public FigureEllipse(double RadiusX, double RadiusY, Brush Color)
            : this(RadiusX, RadiusY)
        {
            this.Color = Color;
        }

        public override double GetSquar()
        {
            return System.Math.PI * ((Width / 2) * (Height / 2));
        }
    }
}
