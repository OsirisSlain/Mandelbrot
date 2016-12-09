using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Threading.Tasks;

namespace Mandelbrot
{
    class Builder
    {
        private readonly object _bitmapLock = new object();

        private readonly int _width;
        private readonly int _height;
        private readonly int _iterations;

        private readonly double _norm;
        private readonly double _scale;

        public Builder(int width, int height, double extent = 2.0, int iterations = 1024)
        {
            _width = width;
            _height = height;
            _iterations = iterations;

            _norm = extent * extent;
            _scale = 2 * extent / Math.Min(width, height);
        }

        private double MandelbrotValue(Complex c)
        {
            int ii = 0;

            for (var z = new Complex(); ii < _iterations && z.Magnitude < _norm; ii++)
            {
                z = z * z * z * z + c;
            }

            return ii < _iterations
                ? (double)ii / _iterations
                : 0;
        }

        private double PixelValue(int x, int y)
        {
            double scaledX = (x - _width / 2) * _scale;
            double scaledY = (_height / 2 - y) * _scale;
            return MandelbrotValue(new Complex(scaledX, scaledY));
        }

        public Bitmap Generate(Func<double, Color> colorMapper = null)
        {
            if (colorMapper == null)
            {
                colorMapper = ColorMappers.BlackToWhite;
            }

            var bitmap = new Bitmap(_width, _height, PixelFormat.Format32bppArgb);

            Parallel.For(0, _height, y =>
            {
                for (int x = 0; x < _width; x++)
                {
                    var pixelColor = colorMapper(PixelValue(x, y));
                    lock (_bitmapLock)
                    {
                        bitmap.SetPixel(x, y, pixelColor);
                    }
                }
            });

            return bitmap;
        }

        public void GenerateFile(string filename, Func<double, Color> colorMapper = null)
        {
            using (var bitmap = Generate(colorMapper))
            {
                bitmap.Save(filename, ImageFormat.Png);
            }
        }
    }
}