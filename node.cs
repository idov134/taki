using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace taki
{
    public class node<T>
    {
        private T info;
        private node<T> next;
        private Image img;
        public node(T x,Image img)
        {
            info = x;
            this.img = img;
            this.next = null;
        }
        public node(T x, node<T> next, Image img)
        {
            this.img = img;
            info = x;
            this.next = next;
        }
        public node<T> getnext()
        {
            return this.next;
        }
        public void setnext(node<T> next)
        {
            this.next = next;
        }
        public T getinfo()
        {
            return this.info;
        }
        public Image getimage()
        {
            return this.img;
        }
        public void setinfo(T x)
        {
            this.info = x;
        }
        public override string ToString()
        {
            return this.info.ToString();
        }
    }
}
