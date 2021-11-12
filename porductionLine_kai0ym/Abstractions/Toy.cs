using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace porductionLine_kai0ym.Abstractions
{
   public abstract class Toy:Label
    {

        public Toy()
        {
            AutoSize = false;
            Width = 50;
            Height = 50;
            Paint += Toy_Paint;
        }

        private void Toy_Paint(object sender, PaintEventArgs e)
        {
            DrawImage(e.Graphics);
        }

        protected abstract void DrawImage(Graphics g);
        

        public virtual void MoveToy()
        {
            Left += 1;
        }
    }
}
