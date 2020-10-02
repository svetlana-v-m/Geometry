using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary_CalculateFiguresArea
{
    public class Rectangle : Shape
    {
        public double A
        {
           get { return fd.a; }
            set { fd.a = value; }
        }

        public double B
        {
            get { return fd.b; }
            set { fd.b=fd.c= value; }
        }
        
        
        public Rectangle(FiguresData fd) : base(fd)
        {
            base.fd.b = base.fd.c;
            base.fd.type = Figures.Rectangle;
        }
        public override double Area()
        {
            return A * B;
        }
    }
}
