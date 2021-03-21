using CustomGeometry;
using System.Windows.Media;

namespace Figures
{
    internal class FigureQuadre : Figure
    {
        public override double Width
        {
            get => base.Width;
            set => base.Height = base.Width = value;
        }

        public override double Height
        {
            get => base.Width;
            set => base.Width = value;
        }

        public FigureQuadre()
            :base(new GeometricQuadre()) { }

        public FigureQuadre(double Size)
            : this()
        {
            Width = Size;
        }

        public FigureQuadre(double Size, Brush Color)
            : this(Size)
        {
            this.Color = Color;
        }

        public override double GetSquar()
        {
            return Width * Height;
        }
    }
}
