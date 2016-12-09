using System;
using System.Diagnostics;

namespace Mandelbrot
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("error -- exactly 1 argument required: filename");
                return;
            }

            var fileArg = args[0];

            if (String.IsNullOrWhiteSpace(fileArg))
            {
                Console.WriteLine("error -- filename cannot be whitespace");
                return;
            }

            var fileExtension = ".png";
            var filename = args[0].EndsWith(fileExtension)
                ? fileArg
                : fileArg + fileExtension;

            Console.WriteLine("Generating fractal " + filename);
            var timer = new Stopwatch();
            timer.Start();

            new Builder(3840, 2160).GenerateFile(filename, ColorMappers.BlackToBlue);

            timer.Stop();
            Console.WriteLine("Fractal generated in " + timer.ElapsedMilliseconds + "ms");
        }
    }
}