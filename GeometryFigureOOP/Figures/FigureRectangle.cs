using CustomGeometry;
using System.Windows.Media;

namespace Figures
{
    internal class FigureRectangle : Figure
    {
        public FigureRectangle()
            : base(new GeometricRectangle()) { }

        public FigureRectangle(double Width, double Height)
            : this()
        {
            base.Width = Width;
            base.Height = Height;
        }

        public FigureRectangle(double Width, double Height, Brush Color)
            : this(Width, Height)
        {
            this.Color = Color;
        }

        public override double GetSquar()
        {
            return Width * Height;
        }
    }
}
