using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace taki
{
    class objgraph
    {
        private TakiTypes currentType; //סוג האובייקט הזה
        private Bitmap pieceImage; // הדימוי של האובייקט הזה
        private Bitmap backPieceImage;
        private bool isTurned;
        private Takicolor color;
        private Takispecial special;
        private int number;
        // מיקום ברירת מחדל לתצוגה
        private Rectangle targetRectangle =
           new Rectangle(0, 0, 89, 129);



        // construct piece
        public objgraph(TakiTypes type, int xLocation,
           int yLocation, Bitmap sourceImage, Bitmap backSourceImage)
        {
            isTurned = true;
            currentType = type; // הגדר סוג נוכחי
            targetRectangle.X = xLocation; // הגדר מיקום x הנוכחי
            targetRectangle.Y = yLocation; // הגדר מיקום y הנוכחי

            // obtain pieceImage from section of sourceImage
            pieceImage = sourceImage.Clone(
               new Rectangle(0, 0, 89, 129), //גודל ההודעה שצריך לערוך
               System.Drawing.Imaging.PixelFormat.DontCare);
            backPieceImage = backSourceImage.Clone(
               new Rectangle(0, 0, 89, 129), // גודל ההודעה שצריך לערוך
               System.Drawing.Imaging.PixelFormat.DontCare);
            SetLocation(xLocation, yLocation);
            string st = currentType.ToString();
            if (st.ElementAt(0) == 'r')
                COLOR = Takicolor.red;
            else if (st.ElementAt(0) == 'g')
                    COLOR = Takicolor.green;
            else if (st.ElementAt(0) == 'b')
                COLOR = Takicolor.blue;
            else
                COLOR = Takicolor.yellow;
            Console.WriteLine(st);
            NUMBER = int.Parse(st.Substring((COLOR == Takicolor.red) ? 3 : (COLOR == Takicolor.blue) ? 4 : (COLOR == Takicolor.green) ? 5 : 6));
            SPECIAL = (NUMBER == 10) ? Takispecial.taki : (NUMBER == 11) ? Takispecial.stop :
                (NUMBER == 12) ? Takispecial.plus :  (NUMBER == 14) ? Takispecial.changecolor :
                (NUMBER == 15) ? Takispecial.takiallcolors : Takispecial.none;
        } // end method ChessPiece

        // draw chess piece
        public void Draw(Graphics graphicsObject)
        {
            graphicsObject.DrawImage((isTurned) ? backPieceImage : pieceImage, targetRectangle);
        } // end method Draw

        // obtain this piece's location rectangle
        public Rectangle GetBounds()
        {
            return targetRectangle;
        } // end method GetBounds

        // set this piece's location
        public void SetLocation(int xLocation, int yLocation)
        {
            targetRectangle.X = xLocation;
            targetRectangle.Y = yLocation;
        } // end method SetLocation 

        public TakiTypes CURRENT_TYPE
        {
            get
            {
                return currentType;
            }
        }

        public bool TURNED
        {
            get
            {
                return (isTurned);
            }
            set
            {
                isTurned = value;
            }

        }
        public Takicolor COLOR
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        public Takispecial SPECIAL
        {
            get
            {
                return special;
            }
            set
            {
                special = value;
            }
        }
        public int NUMBER
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
            }
        }
    }
}
