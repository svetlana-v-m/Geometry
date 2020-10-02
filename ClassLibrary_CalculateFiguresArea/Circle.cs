using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_CalculateFiguresArea
{
    public class Circle : Shape
    {
        public double R
        {
            get { return fd.a; }
            set { fd.a = fd.b = fd.c = value; }
        }
        
        public Circle(FiguresData fd):base(fd)
        {
            base.fd.c = base.fd.b = base.fd.a;
            base.fd.type = Figures.Circle;
        }
        public override double Area()
        {
            return Math.PI * R * R;
        }
    }
}
