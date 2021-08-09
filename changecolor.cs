using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace taki
{
    class changecolor
    {
        private Change currentType; //סוג האובייקט הזה
        private Bitmap pieceImage; // הדימוי של האובייקט הזה

        // מיקום ברירת מחדל לתצוגה
        private Rectangle targetRectangle =
           new Rectangle(0, 0, 180, 70);
        private Rectangle[] arr;


        // construct piece
        public changecolor(Change type, int xLocation,
           int yLocation, Bitmap sourceImage)
        {
            arr = new Rectangle[4];
            arr[0] = new Rectangle(7, 53,36,10);//red
            arr[1] = new Rectangle(50, 53, 36,10);//blue
            arr[2] = new Rectangle(90, 53, 36, 10);//green
            arr[3] = new Rectangle(132, 53, 36, 10);//yellow
            currentType = type; // הגדר סוג נוכחי
            //targetRectangle.X = xLocation; // הגדר מיקום x הנוכחי
            //targetRectangle.Y = yLocation; // הגדר מיקום y הנוכחי

            // obtain pieceImage from section of sourceImage
            pieceImage = sourceImage.Clone(
               targetRectangle, //גודל ההודעה שצריך לערוך
               System.Drawing.Imaging.PixelFormat.DontCare);
            SetLocation(xLocation, yLocation);
        } // end method ChessPiece

        // draw chess piece
        public void Draw(Graphics graphicsObject)
        {
            graphicsObject.DrawImage(pieceImage, targetRectangle);
        }
        public void SetLocation(int xLocation, int yLocation)
        {
            targetRectangle.X = xLocation;
            targetRectangle.Y = yLocation;
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].X += xLocation;
                arr[i].Y += yLocation;
            }
        }
        public Takicolor Getcolor(Point p)
        {
            int i;
            bool found=false;
            for (i = 0; i < arr.Length&&!found;)
            {
                if (arr[i].Contains(p))
                {
                    found = true;
                }
                else
                {
                    i++;
                }
            }
            return (i == 0) ? Takicolor.red : (i == 1) ? Takicolor.blue : (i == 2) ? Takicolor.green : (i == 3) ? Takicolor.yellow : Takicolor.none;
        }
    }
}
