using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Drawing;

namespace taki
{
    class kupa
    {
        private mylist<card> st;
        private Image img;
        private node<card> pos;
        private int size;
        public kupa()
        {
            this.st = new mylist<card>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    //pos=new node<card>(0,pos,Image.FromFile(i + 1 + "/" + (j + 1) + ".png"));
                    //st.getinfo() = Image.FromFile(i + 1 + "/" + (j + 1) + ".png");
                }
            }
            this.img = Image.FromFile("kupa.png");
        }
        public void setimg(string img)
        {
            this.img = Image.FromFile(img + ".png");
        }
        public int getsize()
        {
            return st.getsize();
        }
    }
}
