using porductionLine_kai0ym.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace porductionLine_kai0ym.Entities
{
    class Car : Abstractions.Toy
    {
        protected override void DrawImage(Graphics g)
        {
            Image imageFile = Image.FromFile("Images/car.png");
            g.DrawImage(imageFile, new Rectangle(0, 0, Width, Height));
        }
    }
}
