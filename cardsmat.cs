using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace taki
{
    class cardsmat
    {

        private card[,] cardmat;
        public cardsmat()
        {
            this.cardmat = new card[4,15];
            int x = 5;
            int y = 150;
            Image pic;
            for (int i = 0; i < cardmat.GetLength(0); i++)
            {
                for (int j = 0; j < cardmat.GetLength(1); j++)
                {
                    card cd = new card();
                    cd.SetX(x);
                    cd.SetY(y);
                    cd.SetNum(j + 1);
                    pic = Image.FromFile(i + 1 + "/" + (j + 1) + ".png");
                    cd.SetPic(pic);
                    cardmat[i, j] = cd;
                    x = x + 90;
                }

                x = 5;
                y = y + 132;
            }
        }
    }
}
