using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Taki
{
    class Hand:List<TakiGraphicsItem>
    {
        private int _yTopLeft;
        private int _xTopLeft;

        public Hand(int xTopLeft, int yTopLeft)
        {
            _xTopLeft = xTopLeft;
            _yTopLeft = yTopLeft;
        }

        public void AddCard(TakiGraphicsItem card)
        {
            Add(card);
            card.SetLocation(_xTopLeft + (Count - 1) * card.GetBounds().Width, _yTopLeft);
        }
    }
}
