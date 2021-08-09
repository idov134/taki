using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace taki
{
    class deck: List<objgraph> 
    {
        private Rectangle rect;
        private List<Bitmap> imageList;
        private Bitmap backCardImage;
        public static Random rnd;

        public deck(int x,int y)
        {
            rnd = new Random();
            imageList = new List<Bitmap>();
            int count = 0;
            backCardImage = (Bitmap)Image.FromFile(@"Images\BackCard.png");
            rect = new Rectangle(x, y,89,129);
            foreach (var cardImage in Enum.GetValues(typeof(TakiTypes)))
            {
                if ((TakiTypes)cardImage != TakiTypes.BackCard)
                {
                    imageList.Add((Bitmap)Image.FromFile(@"Images\" + cardImage.ToString() + ".png"));
                    //System.Console.WriteLine(cardImage.ToString());
                    this.Add(new objgraph((TakiTypes)cardImage, 70, 80, imageList.ElementAt(imageList.Count - 1), backCardImage));
                    this.ElementAt(this.Count - 1).SetLocation(x, y);
                    this.ElementAt(this.Count - 1).TURNED = true;
                }//make class name hand she henharit from list and has all the qeualetis which gcardlist has
            }
        }

        public void Draw(Graphics g)
        {
            if (this.Count == 1)
            {
                foreach (var cardImage in Enum.GetValues(typeof(TakiTypes)))
                {
                    if ((TakiTypes)cardImage != TakiTypes.BackCard)
                    {
                        imageList.Add((Bitmap)Image.FromFile(@"Images\" + cardImage.ToString() + ".png"));
                        //System.Console.WriteLine(cardImage.ToString());
                        this.Add(new objgraph((TakiTypes)cardImage, 70, 80, imageList.ElementAt(imageList.Count - 1), backCardImage));
                        this.ElementAt(this.Count - 1).SetLocation(rect.X, rect.Y);
                        this.ElementAt(this.Count - 1).TURNED = true;
                    }//make class name hand she henharit from list and has all the qeualetis which gcardlist has
                }
            }
            this.ElementAt(this.Count-1).Draw(g);
        }
        public void Shuffle()
        {
            int indexcard1;
            int indexcard2;
            int tempindex;
            objgraph card1,card2;
            for (int i = 0; i < 200; i++)
            {
                indexcard1 = rnd.Next(this.Count);
                indexcard2 = rnd.Next(this.Count);
                while (indexcard1 == indexcard2)
                {
                    indexcard1 = rnd.Next(this.Count);
                    indexcard2 = rnd.Next(this.Count);
                }
                if (indexcard1 > indexcard2)
                {
                    tempindex = indexcard1;
                    indexcard1 = indexcard2;
                    indexcard2 = tempindex;
                }
                card2 = this.ElementAt(indexcard2);
                this.RemoveAt(indexcard2);
                card1 = this.ElementAt(indexcard1);
                this.RemoveAt(indexcard1);
                Insert(indexcard1, card2);
                Insert(indexcard2, card1);
            }
        }
        public objgraph Getcard
        {
            get
            {
                objgraph card = this.ElementAt(this.Count - 1);
                this.Remove(card);
                return card;
            }

        }
        public Rectangle GetBounds()
        {
            return rect;
        } 
    }
}
