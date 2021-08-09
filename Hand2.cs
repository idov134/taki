using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace taki
{
    class Hand2 : List<objgraph> 
    {
        private Rectangle rect;

        public Hand2(int x,int y)
        {
            rect = new Rectangle(x, y,900,89);
        }

        public void Draw(Graphics g)
        {
            int i;
            for (i = 0; i < this.Count; i++)
            {
                this.ElementAt(i).SetLocation(rect.X+i*90,rect.Y);
                this.ElementAt(i).Draw(g);
            }
        }
        public void reverse(bool turned)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this.ElementAt(i).TURNED = turned;
            }
        }

    }
}
