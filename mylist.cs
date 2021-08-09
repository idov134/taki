using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace taki
{
    public class mylist<T>
    {
        private int size;
        private node<T> first;
        public mylist()
        {
            size = 0;
            this.first = null;
        }
        public node<T> getfirst()
        {
            return this.first;
        }

        public node<T> insert(node<T> pos, T x,Image img)
        {
            size++;
            node<T> temp = new node<T>(x, img);
            if (pos == null)
            {
                temp.setnext(this.first);
                this.first = temp;
            }
            else
            {
                temp.setnext(pos.getnext());
                pos.setnext(temp);
            }
            return temp;
        }

        public node<T> remove(node<T> pos)
        {
            size--;
            if (this.first == pos)
                this.first = pos.getnext();
            else
            {
                node<T> prevpos = this.getfirst();
                while (prevpos.getnext() != pos)
                    prevpos = prevpos.getnext();
                prevpos.setnext(pos.getnext());
            }
            node<T> nextpos = pos.getnext();
            pos.setnext(null);
            return nextpos;
        }
        public int getsize()
        {
            return this.size;
        }
    }
}
