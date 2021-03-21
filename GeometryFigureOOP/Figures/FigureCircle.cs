using CustomGeometry;
using System.Windows.Media;

namespace Figures
{
    internal class FigureCircle : Figure
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

        public FigureCircle()
            : base(new GeometricCircle()) { }

        public FigureCircle(double Radius)
            : this()
        {
            Width = Radius;
        }

        public FigureCircle(double Radius, Brush Color)
            : this(Radius)
        {
            this.Color = Color;
        }

        public override double GetSquar()
        {
            return System.Math.PI * System.Math.Pow((Width / 2), 2);
        }
    }
}
