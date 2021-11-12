using porductionLine_kai0ym.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace porductionLine_kai0ym.Entities
{
    class Toy: Abstractions.Toy
    {

        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }
    }
}
