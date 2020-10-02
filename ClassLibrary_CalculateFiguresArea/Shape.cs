using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary_CalculateFiguresArea
{
    public abstract class Shape
    {
        protected FiguresData fd;

        public Shape(FiguresData fd)
        {
            this.fd = fd;
        }

        public abstract double Area();
    }
}
