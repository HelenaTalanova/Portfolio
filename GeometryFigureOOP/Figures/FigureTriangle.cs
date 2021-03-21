using CustomGeometry;
using System.Windows.Media;

namespace Figures
{
    internal class FigureTriangle : Figure
    {
        public FigureTriangle()
            : base(new GeometricTriangle()) { }

        public FigureTriangle(double Width, double Height)
            : this()
        {
            base.Width = Width;
            base.Height = Height;
        }

        public FigureTriangle(double Width, double Height, Brush Color)
            : this(Width, Height)
        {
            this.Color = Color;
        }

        public override double GetSquar()
        {
            return (Width * Height) / 2;
        }
    }
}
