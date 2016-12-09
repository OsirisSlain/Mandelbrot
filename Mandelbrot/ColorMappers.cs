using System;
using System.Drawing;

namespace Mandelbrot
{
    static class ColorMappers
    {
        public static Color BlackToWhite(double normalizedPixel)
        {
            int _maxColor = 255;
            double _contrast = 0.8;

            int colorValue = (int)(_maxColor * Math.Pow(normalizedPixel, _contrast));

            return Color.FromArgb(colorValue, colorValue, colorValue);
        }

        public static Color BlackToBlue(double normalizedPixel)
        {
            int _maxColor = 255;
            double _contrast = 0.5;

            int colorValue = (int)(_maxColor * Math.Pow(normalizedPixel, _contrast));

            return Color.FromArgb(0, 0, colorValue);
        }

        public static Color BlackToGreen(double normalizedPixel)
        {
            int _maxColor = 255;
            double _contrast = 0.5;

            int colorValue = (int)(_maxColor * Math.Pow(normalizedPixel, _contrast));

            return Color.FromArgb(0, colorValue, 0);
        }
    }
}