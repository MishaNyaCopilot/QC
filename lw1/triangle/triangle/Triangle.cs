using System;
using System.Collections.Generic;

namespace triangle
{
    public class Triangle
    {
        public Triangle(double a, double b, double c)
        {
            if (a <= 0 || b <= 0 || c <= 0
                || a > MaxSize || b > MaxSize || c > MaxSize)
            {
                throw new ArgumentException("Неизвестная ошибка");
            }
            
            if (a + b <= c 
                || b + c <= a 
                || c + a <= b)
            {
                throw new ArgumentException("Не треугольник");
            }
            
            _a = a;
            _b = b;
            _c = c;
        }
        
        public bool IsEquilateralTriangle()
        {
            return _a.CompareTo(_b) == 0 && _b.CompareTo(_c) == 0;
        }

        public bool IsIsoscelesTriangle()
        {
            return _a.CompareTo(_b) == 0 
                   || _a.CompareTo(_c) == 0 
                   || _c.CompareTo(_b) == 0;
        }

        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        private const double MaxSize = 1000000;
    }
}