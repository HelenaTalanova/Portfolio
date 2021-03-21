using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomGeometry
{
    /// <summary>
    /// Перечисление типов фигур
    /// </summary>
    internal enum GeometricFigures
    {
        Quadre,
        Rectangle,
        Circle,
        Ellipse,
        Triangle,
    }

    /// <summary>
    /// Методы расширения для "GeometricFigures"
    /// </summary>
    internal static class GeometricFiguresExtension
    {
        internal static string ToText(this GeometricFigures value)
        {
            switch (value)
            {
                case GeometricFigures.Rectangle:
                    return "Прямоугольник";
                case GeometricFigures.Quadre:
                    return "Квадрат";
                case GeometricFigures.Circle:
                    return "Круг";
                case GeometricFigures.Ellipse:
                    return "Эллипс";
                case GeometricFigures.Triangle:
                    return "Треугольник";
            }
            throw new Exception($"Неизвестный тип геометрической фигуры - {value}");
        }
    }
}
