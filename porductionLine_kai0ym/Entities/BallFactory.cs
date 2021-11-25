using porductionLine_kai0ym.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace porductionLine_kai0ym.Entities
{
    class BallFactory:IToyFactory
    {
        public Abstractions.Toy CreateNew()
        {
            return new Ball();
        }
        public Color BallColor { get; set; }

        //public Toy CreateNew()
        //{
        //    return new Ball(BallColor);
        //}


    }
}
