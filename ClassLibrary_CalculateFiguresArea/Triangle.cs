using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_CalculateFiguresArea
{
    public class Triangle : Shape
    {
        public double A
        {
            get { return fd.a; }
            set { fd.a = value; }
        }

        public double B
        {
            get { return fd.b; }
            set { fd.b = value; }
        }

        public double C
        {
            get { return fd.c; }
            set { fd.c = value; }
        }

        public Triangle(FiguresData fd):base(fd)
        {
            base.fd.type = Figures.Triangle;
        }

        public override double Area()
        {
            /*Проверка на существование треугольника с такими длинами сторон*/
            if ((A + B) > C || (B + C) > A || (A + C) > B)
            {
                /*Проверка треугольника на прямоугольность*/
                if (C * C == A * A + B * B) return 0.5 * A * B;
                else if (A * A == B * B + C * C) return 0.5 * B * C;
                else if (B * B == A * A + C * C) return 0.5 * A * C;
                /*Если треугольник не прямоугольный, площадь вычисляется поформуле Герона*/
                else { double p = (A + B + C) / 2; return Math.Sqrt(p * (p - A) * (p - B) * (p - C)); }
            }
            /*Если треугольник с введенными сторонами не существует, площадь равна 0*/
            else return 0;
        }
        /// <summary>
        /// Расчет длины третьей стороны треугольника по двум сторонам и углу между ними
        /// </summary>
        /// <param name="a">Длина стороны a</param>
        /// <param name="b">Длина стороны b</param>
        /// <param name="angle">Угол между сторонами a и b</param>
        /// <returns></returns>
        public double CalculateSideLength(double a,double b,double angle)
        {
            return Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2) - 2 * a * b * Math.Cos(angle));
        }
    }
}
