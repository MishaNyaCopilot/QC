using System;

namespace triangle
{
    internal static class Program
    {
        
        public static void Main(string[] args)
        {
            try
            {
                if (args.Length != 3)
                {
                    throw new Exception("Usage: triangle.exe <a> <b> <c>");
                }

                double a, b, c;
                
                if (!double.TryParse(args[0], out a) || !double.TryParse(args[1], out b) || !double.TryParse(args[2], out c))
                {
                    throw new ArgumentException("Неизвестная ошибка");
                }

                var triangle = new Triangle(a, b, c);
                if (triangle.IsEquilateralTriangle())
                {
                    Console.WriteLine("Равносторонний");
                    return;
                }

                if (triangle.IsIsoscelesTriangle())
                {
                    Console.WriteLine("Равнобедренный");
                    return;
                }
                
                Console.WriteLine("Обычный");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}