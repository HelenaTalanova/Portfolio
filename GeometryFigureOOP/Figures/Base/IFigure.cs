using System.Windows.Media;

namespace Figures
{
    interface IFigure
    {
        Brush Color { get; set; }
        double Width { get; set; }
        double Height { get; set; }
    }
}
