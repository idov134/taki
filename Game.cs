using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace taki
{
    public partial class Game : Form
    {
        string username;
        Bitmap backCardImage;
        objgraph onboard;
        Hand2 hand;
        Hand2 comp;
        deck takiDeck;
        bool turn;//true human, false comp
        bool victory;// false= no victory yet, true= someone won
        Graphics g;
        int x = 250;
        int y = 100;
        public Form1 fr1 = new Form1();
        int timesofnotplay;
        int turns;
        Random rnd;
        Takispecial special;
        Takicolor color;
        Bitmap changeimage;
        bool showchange;
        private changecolor change;
        public Game()
        {
            InitializeComponent();
            changeimage = (Bitmap)Image.FromFile(@"Images\" + Change.changecolor.ToString() + ".png");
            showchange = false;
            change = new changecolor(Change.changecolor, 400, 300, changeimage);
            int count = 0;
            turns = 0;
            turn = true;
            username = "shlomo";
            victory = false;
            special = Takispecial.none;
            color = Takicolor.blue;
            timesofnotplay = 0;
            takiDeck = new deck(100, 200);
            takiDeck.Shuffle();
            rnd = new Random();
            hand = new Hand2(250, 550);
            comp = new Hand2(250, 250);
            for (count = 0; count < 8; count++)
            {
                hand.Add(takiDeck.Getcard);
                hand.reverse(false);
                comp.Add(takiDeck.Getcard);
                comp.reverse(true);
            }

            onboard = takiDeck.Getcard;
            while (onboard.SPECIAL == Takispecial.changecolor || onboard.SPECIAL == Takispecial.takiallcolors)
            {
                onboard = takiDeck.Getcard;
            }
            onboard.TURNED = false;
            onboard.SetLocation(600, 400);

            timer1.Start();
        }

        private void Game_Load(object sender, EventArgs e)
        {


        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            fr1.Show();
        }

        private void picpaint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }



        private void textBox1_TextChanged(object sender, EventArgs e, int counter)
        {
            //this.Text = counter + " cards in the kupa";
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.fr1.Show();
            this.Close();
        }

        private void PickPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            hand.Draw(g);
            onboard.Draw(g);
            comp.Draw(g);
            takiDeck.Draw(g);
            if (showchange)
            {
                change.Draw(g);
            }
            if (victory)
            {
                DB database = new DB(turns);
                database.Show();
                this.Hide();
            }
        }

        private void PickMouseDown(object sender, MouseEventArgs e)
        {
            bool touched = false;
            bool found = false;
            bool check = false;
            int i, random_color, counter = 0;
            if (!victory)
            {
                if (turn == true)//human turn
                {
                    if (showchange)
                    {
                        color = change.Getcolor(new Point(e.X, e.Y));
                        if (color != Takicolor.none)
                        {
                            onboard.COLOR = color;
                            turn = !turn;
                            showchange = false;
                        }
                        pictureBox19.Refresh();
                    }
                    else if (takiDeck.GetBounds().Contains(e.X, e.Y))
                    {
                        if (hand.Count < 8)
                        {
                            hand.Add(takiDeck.Getcard);
                            hand.reverse(false);
                            pictureBox19.Refresh();
                        }
                        else
                        {
                            turn = !turn;
                            turns++;
                            special = Takispecial.none;
                            timesofnotplay++;
                        }
                    }
                    else
                    {
                        found = false;
                        for (i = 0; i < hand.Count && !found; )
                        {
                            if (hand.ElementAt(i).GetBounds().Contains(e.X, e.Y))
                            {
                                touched = true;
                                //none
                                if ((special == Takispecial.none) && (hand.ElementAt(i).SPECIAL == Takispecial.changecolor || hand.ElementAt(i).SPECIAL == Takispecial.takiallcolors))
                                {
                                    special = hand.ElementAt(i).SPECIAL;
                                    color = onboard.COLOR;
                                    found = true;
                                }
                                else if ((special == Takispecial.none) && (hand.ElementAt(i).COLOR == onboard.COLOR || hand.ElementAt(i).NUMBER == onboard.NUMBER || (hand.ElementAt(i).SPECIAL == onboard.SPECIAL && hand.ElementAt(i).SPECIAL != Takispecial.none || (hand.ElementAt(i).SPECIAL == Takispecial.takiallcolors && onboard.SPECIAL == Takispecial.taki) || (hand.ElementAt(i).SPECIAL == Takispecial.taki && onboard.SPECIAL == Takispecial.takiallcolors))))
                                {
                                    special = hand.ElementAt(i).SPECIAL;
                                    found = true;
                                }
                                //plus
                                else if (special == Takispecial.plus && hand.ElementAt(i).SPECIAL == Takispecial.taki && hand.ElementAt(i).COLOR == onboard.COLOR)
                                {
                                    special = Takispecial.taki;
                                    found = true;
                                }
                                else if (special == Takispecial.plus && hand.ElementAt(i).SPECIAL == Takispecial.none && hand.ElementAt(i).COLOR == onboard.COLOR)
                                {
                                    special = Takispecial.none;
                                    found = true;
                                }
                                else if (special == Takispecial.plus && hand.ElementAt(i).SPECIAL == Takispecial.stop && hand.ElementAt(i).COLOR == onboard.COLOR)
                                {
                                    special = Takispecial.stop;
                                    found = true;
                                }
                                else if (special == Takispecial.plus && hand.ElementAt(i).SPECIAL == Takispecial.takiallcolors)
                                {
                                    special = Takispecial.takiallcolors;
                                    color = onboard.COLOR;
                                    found = true;
                                }
                                else if (special == Takispecial.plus && hand.ElementAt(i).SPECIAL == Takispecial.changecolor)
                                {
                                    special = Takispecial.changecolor;
                                    found = true;
                                }
                                else if (special == Takispecial.plus && hand.ElementAt(i).SPECIAL == Takispecial.plus)
                                {
                                    found = true;
                                }
                                //stop
                                else if (special == Takispecial.stop && hand.ElementAt(i).SPECIAL == Takispecial.stop)
                                {
                                    found = true;
                                }
                                else if (special == Takispecial.stop && hand.ElementAt(i).SPECIAL == Takispecial.plus && hand.ElementAt(i).COLOR == onboard.COLOR)
                                {
                                    special = Takispecial.plus;
                                    found = true;
                                }
                                else if (special == Takispecial.stop && hand.ElementAt(i).SPECIAL == Takispecial.taki && hand.ElementAt(i).COLOR == onboard.COLOR)
                                {
                                    special = Takispecial.taki;
                                    found = true;
                                }
                                else if (special == Takispecial.stop && hand.ElementAt(i).SPECIAL == Takispecial.none && hand.ElementAt(i).COLOR == onboard.COLOR)
                                {
                                    special = Takispecial.none;
                                    found = true;
                                }
                                else if (special == Takispecial.stop && hand.ElementAt(i).SPECIAL == Takispecial.takiallcolors)
                                {
                                    special = Takispecial.takiallcolors;
                                    color = onboard.COLOR;
                                    found = true;
                                }
                                else if (special == Takispecial.stop && hand.ElementAt(i).SPECIAL == Takispecial.changecolor)
                                {
                                    special = Takispecial.changecolor;
                                    found = true;
                                }
                                //taki
                                else if (special == Takispecial.taki && hand.ElementAt(i).SPECIAL == Takispecial.changecolor)
                                {
                                    found = true;
                                    hand.ElementAt(i).COLOR = onboard.COLOR;
                                }
                                else if (special == Takispecial.taki && hand.ElementAt(i).NUMBER == onboard.NUMBER && hand.ElementAt(i).COLOR != onboard.COLOR)
                                {
                                    special = hand.ElementAt(i).SPECIAL;
                                    found = true;
                                }
                                else if (special == Takispecial.taki && hand.ElementAt(i).SPECIAL == Takispecial.taki)
                                {
                                    found = true;
                                }
                                else if (special == Takispecial.taki && hand.ElementAt(i).SPECIAL == Takispecial.takiallcolors)
                                {
                                    special = Takispecial.takiallcolors;
                                    color = onboard.COLOR;
                                    found = true;
                                }
                                else if (special == Takispecial.taki && hand.ElementAt(i).COLOR == onboard.COLOR)
                                {
                                    found = true;
                                }
                                //taki all colors
                                else if (special == Takispecial.takiallcolors && hand.ElementAt(i).SPECIAL == Takispecial.changecolor)
                                {
                                    found = true;
                                }
                                else if (special == Takispecial.takiallcolors && hand.ElementAt(i).NUMBER == onboard.NUMBER && hand.ElementAt(i).COLOR != color)
                                {
                                    special = hand.ElementAt(i).SPECIAL;
                                    found = true;
                                }
                                else if (special == Takispecial.takiallcolors && hand.ElementAt(i).SPECIAL == Takispecial.taki && onboard.SPECIAL == Takispecial.takiallcolors)
                                {
                                    special = Takispecial.taki;
                                    found = true;
                                }
                                else if (special == Takispecial.takiallcolors && hand.ElementAt(i).SPECIAL == Takispecial.taki && onboard.SPECIAL != Takispecial.takiallcolors && onboard.COLOR == hand.ElementAt(i).COLOR)
                                {
                                    special = Takispecial.taki;
                                    found = true;
                                }
                                else if (special == Takispecial.takiallcolors && hand.ElementAt(i).COLOR == color)
                                {
                                    found = true;
                                }
                                // change color
                                else if (special == Takispecial.changecolor && (hand.ElementAt(i).SPECIAL == Takispecial.changecolor || hand.ElementAt(i).SPECIAL == Takispecial.takiallcolors))
                                {
                                    special = hand.ElementAt(i).SPECIAL;
                                    color = onboard.COLOR;
                                    found = true;
                                }

                                else if (special == Takispecial.changecolor && (hand.ElementAt(i).COLOR == onboard.COLOR || hand.ElementAt(i).NUMBER == onboard.NUMBER))//valid
                                {

                                    special = hand.ElementAt(i).SPECIAL;
                                    found = true;
                                }
                                else
                                {
                                    i++;
                                }

                            }
                            else //not valid
                            {
                                i++;

                            }
                        }

                        if (found)
                        {
                            timesofnotplay = 0;
                            onboard = hand.ElementAt(i);
                            onboard.SetLocation(600, 400);
                            hand.Remove(hand.ElementAt(i));
                            if (special == Takispecial.takiallcolors)
                            {
                                onboard.COLOR = color;
                            }
                            else if (special == Takispecial.changecolor)
                            {
                                showchange = true;
                                pictureBox19.Refresh();
                            }
                            else if (special == Takispecial.none)
                            {
                                turn = !turn;
                                turns++;
                            }
                            if (special == Takispecial.taki || special == Takispecial.takiallcolors)
                            {
                                for (int j = 0; j < hand.Count; j++)
                                {
                                    if (hand.ElementAt(j).COLOR == onboard.COLOR || (onboard.SPECIAL != Takispecial.none && hand.ElementAt(j).SPECIAL == onboard.SPECIAL) || 
                                        (onboard.SPECIAL == Takispecial.none && hand.ElementAt(j).NUMBER == onboard.NUMBER && hand.ElementAt(j).COLOR != color) || 
                                        (onboard.SPECIAL == Takispecial.taki && hand.ElementAt(j).SPECIAL == Takispecial.takiallcolors) || 
                                        (onboard.SPECIAL == Takispecial.takiallcolors && hand.ElementAt(j).SPECIAL == Takispecial.taki) || (hand.ElementAt(j).SPECIAL == Takispecial.changecolor))
                                    {
                                        counter++;
                                    }
                                }
                                if (counter == 0)// he dont have more cards to play
                                {
                                    special = onboard.SPECIAL;
                                    turn = !turn;
                                    turns++;
                                    special = Takispecial.none;
                                }
                            }
                        }
                        else
                        {
                            if (touched)
                                MessageBox.Show("not valid card");
                        }
                    }
                }
                //    else//comp turn
                //    {
                //        found = false;
                //        for (i = 0; i < comp.Count && !found; )
                //        {
                //            //check valid
                //            //none
                //            if ((special == Takispecial.none) && (comp.ElementAt(i).SPECIAL == Takispecial.changecolor || comp.ElementAt(i).SPECIAL == Takispecial.takiallcolors))
                //            {
                //                special = comp.ElementAt(i).SPECIAL;
                //                color = onboard.COLOR;
                //                found = true;
                //            }
                //            else if ((special == Takispecial.none) && (comp.ElementAt(i).COLOR == onboard.COLOR || comp.ElementAt(i).NUMBER == onboard.NUMBER))
                //            {
                //                special = comp.ElementAt(i).SPECIAL;
                //                found = true;
                //            }
                //            //plus
                //            else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.taki && comp.ElementAt(i).COLOR == onboard.COLOR)
                //            {
                //                special = Takispecial.taki;
                //                found = true;
                //            }
                //            else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.none && comp.ElementAt(i).COLOR == onboard.COLOR)
                //            {
                //                special = Takispecial.none;
                //                found = true;
                //            }
                //            else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.stop && comp.ElementAt(i).COLOR == onboard.COLOR)
                //            {
                //                special = Takispecial.stop;
                //                found = true;
                //            }
                //            else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.takiallcolors)
                //            {
                //                special = Takispecial.takiallcolors;
                //                color = onboard.COLOR;
                //                found = true;
                //            }
                //            else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.changecolor)
                //            {
                //                special = Takispecial.changecolor;
                //                found = true;
                //            }
                //            else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.plus)
                //            {
                //                found = true;
                //            }
                //            //stop
                //            else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.stop)
                //            {
                //                found = true;
                //            }
                //            else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.plus && comp.ElementAt(i).COLOR == onboard.COLOR)
                //            {
                //                special = Takispecial.plus;
                //                found = true;
                //            }
                //            else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.taki && comp.ElementAt(i).COLOR == onboard.COLOR)
                //            {
                //                special = Takispecial.taki;
                //                found = true;
                //            }
                //            else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.none && comp.ElementAt(i).COLOR == onboard.COLOR)
                //            {
                //                special = Takispecial.none;
                //                found = true;
                //            }
                //            else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.takiallcolors)
                //            {
                //                special = Takispecial.takiallcolors;
                //                color = onboard.COLOR;
                //                found = true;
                //            }
                //            else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.changecolor)
                //            {
                //                special = Takispecial.changecolor;
                //                found = true;
                //            }
                //            //taki
                //            else if (special == Takispecial.taki && comp.ElementAt(i).SPECIAL == Takispecial.changecolor)
                //            {
                //                found = true;
                //            }
                //            else if (special == Takispecial.taki && comp.ElementAt(i).NUMBER == onboard.NUMBER && comp.ElementAt(i).COLOR != onboard.COLOR)
                //            {
                //                special = comp.ElementAt(i).SPECIAL;
                //                found = true;
                //            }
                //            else if (special == Takispecial.taki && comp.ElementAt(i).SPECIAL == Takispecial.taki)
                //            {
                //                found = true;
                //            }
                //            else if (special == Takispecial.taki && comp.ElementAt(i).SPECIAL == Takispecial.takiallcolors)
                //            {
                //                special = Takispecial.takiallcolors;
                //                color = onboard.COLOR;
                //                found = true;
                //            }
                //            else if (special == Takispecial.taki && comp.ElementAt(i).COLOR == onboard.COLOR)
                //            {
                //                found = true;
                //            }
                //            //taki all colors
                //            else if (special == Takispecial.takiallcolors && comp.ElementAt(i).SPECIAL == Takispecial.changecolor)
                //            {
                //                found = true;
                //            }
                //            else if (special == Takispecial.takiallcolors && comp.ElementAt(i).NUMBER == onboard.NUMBER && comp.ElementAt(i).COLOR != color)
                //            {
                //                special = comp.ElementAt(i).SPECIAL;
                //                found = true;
                //            }
                //            else if (special == Takispecial.takiallcolors && comp.ElementAt(i).SPECIAL == Takispecial.taki && onboard.SPECIAL == Takispecial.takiallcolors)
                //            {
                //                special = Takispecial.taki;
                //                found = true;
                //            }
                //            else if (special == Takispecial.takiallcolors && comp.ElementAt(i).SPECIAL == Takispecial.taki && onboard.SPECIAL != Takispecial.takiallcolors && onboard.COLOR == comp.ElementAt(i).COLOR)
                //            {
                //                special = Takispecial.taki;
                //                found = true;
                //            }
                //            else if (special == Takispecial.takiallcolors && comp.ElementAt(i).COLOR == color)
                //            {
                //                found = true;
                //            }
                //            // change color
                //            else if (special == Takispecial.changecolor && (comp.ElementAt(i).SPECIAL == Takispecial.changecolor || comp.ElementAt(i).SPECIAL == Takispecial.takiallcolors))
                //            {
                //                special = comp.ElementAt(i).SPECIAL;
                //                color = onboard.COLOR;
                //                found = true;
                //            }

                //            else if (special == Takispecial.changecolor && (comp.ElementAt(i).COLOR == onboard.COLOR || comp.ElementAt(i).NUMBER == onboard.NUMBER))
                //            {
                //                special = comp.ElementAt(i).SPECIAL;
                //                found = true;
                //            }
                //            else //not valid
                //            {
                //                i++;
                //            }
                //        }

                //        if (found)
                //        {
                //            onboard = comp.ElementAt(i);
                //            onboard.SetLocation(600, 400);
                //            onboard.TURNED = false;
                //            comp.Remove(comp.ElementAt(i));
                //            if (special == Takispecial.takiallcolors)
                //            {
                //                onboard.COLOR = color;
                //            }
                //            else if (special == Takispecial.changecolor)
                //            {

                //                random_color = rnd.Next(1, 5);
                //                switch (random_color)
                //                {
                //                    case (1):
                //                        color = Takicolor.red;
                //                        break;
                //                    case (2):
                //                        color = Takicolor.blue;
                //                        break;
                //                    case (3):
                //                        color = Takicolor.green;
                //                        break;
                //                    case (4):
                //                        color = Takicolor.yellow;
                //                        break;
                //                }

                //                onboard.COLOR = color;
                //                MessageBox.Show("the computer choose the color " + color);
                //                turn = !turn;
                //            }
                //            else if (special == Takispecial.none)
                //            {
                //                turn = !turn;
                //            }
                //            if (special == Takispecial.taki || special == Takispecial.takiallcolors)
                //            {
                //                for (int j = 0; j < comp.Count && !check; j++)
                //                {
                //                    if (hand.ElementAt(j).COLOR == onboard.COLOR || (onboard.SPECIAL != Takispecial.none && hand.ElementAt(j).SPECIAL == onboard.SPECIAL) || (onboard.SPECIAL == Takispecial.none && hand.ElementAt(j).NUMBER == onboard.NUMBER && hand.ElementAt(j).COLOR != color) || (onboard.SPECIAL == Takispecial.taki && hand.ElementAt(j).SPECIAL == Takispecial.takiallcolors) || (onboard.SPECIAL == Takispecial.takiallcolors && hand.ElementAt(j).SPECIAL == Takispecial.taki) || (hand.ElementAt(j).SPECIAL == Takispecial.changecolor))
                //                    {
                //                        counter++;
                //                        check = !check;
                //                    }
                //                }
                //                if (counter == 0)// he dont have more cards to play
                //                {
                //                    special = onboard.SPECIAL;
                //                    turn = !turn;
                //                    special = Takispecial.none;
                //                }
                //            }
                //        }
                //        else
                //        {
                //            if (comp.Count < 8)
                //            {
                //                comp.Add(takiDeck.Getcard);
                //            }
                //            else
                //            {
                //                timesofnotplay++;
                //                turn = !turn;
                //                special = Takispecial.none;
                //            }
                //        }

                //    }
                //}
                pictureBox19.Refresh();
                if (hand.Count == 0)
                {
                    victory = true;
                    MessageBox.Show(" you won ");
                }
                //if (comp.Count == 0)
                //{
                //    victory = true;
                //    MessageBox.Show(" computer won ");
                //}
                if (timesofnotplay == 2)//both cant play so change the board
                {
                    onboard = takiDeck.Getcard;
                    while (onboard.SPECIAL == Takispecial.changecolor || onboard.SPECIAL == Takispecial.takiallcolors)
                    {
                        onboard = takiDeck.Getcard;
                    }
                    onboard.TURNED = false;
                    onboard.SetLocation(600, 400);
                    timesofnotplay = 0;
                }
            }
            pictureBox19.Refresh();
        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.fr1.Show();
            this.Close();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (victory == false)
            {
                if (turn == false)
                {
                    bool touched = false;
                    bool found = false;
                    bool check = false;
                    int i, random_color, counter = 0;
                    for (i = 0; i < comp.Count && !found; )
                    {
                        //check valid
                        //none
                        if ((special == Takispecial.none) && (comp.ElementAt(i).SPECIAL == Takispecial.changecolor || comp.ElementAt(i).SPECIAL == Takispecial.takiallcolors))
                        {
                            special = comp.ElementAt(i).SPECIAL;
                            color = onboard.COLOR;
                            found = true;
                        }
                        else if ((special == Takispecial.none) && (comp.ElementAt(i).COLOR == onboard.COLOR || comp.ElementAt(i).NUMBER == onboard.NUMBER || (comp.ElementAt(i).SPECIAL == onboard.SPECIAL && comp.ElementAt(i).SPECIAL != Takispecial.none || (comp.ElementAt(i).SPECIAL == Takispecial.takiallcolors && onboard.SPECIAL == Takispecial.taki) || (comp.ElementAt(i).SPECIAL == Takispecial.taki && onboard.SPECIAL == Takispecial.takiallcolors))))
                        {
                            special = comp.ElementAt(i).SPECIAL;
                            found = true;
                        }
                        //plus
                        else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.taki && comp.ElementAt(i).COLOR == onboard.COLOR)
                        {
                            special = Takispecial.taki;
                            found = true;
                        }
                        else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.none && comp.ElementAt(i).COLOR == onboard.COLOR)
                        {
                            special = Takispecial.none;
                            found = true;
                        }
                        else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.stop && comp.ElementAt(i).COLOR == onboard.COLOR)
                        {
                            special = Takispecial.stop;
                            found = true;
                        }
                        else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.takiallcolors)
                        {
                            special = Takispecial.takiallcolors;
                            color = onboard.COLOR;
                            found = true;
                        }
                        else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.changecolor)
                        {
                            special = Takispecial.changecolor;
                            found = true;
                        }
                        else if (special == Takispecial.plus && comp.ElementAt(i).SPECIAL == Takispecial.plus)
                        {
                            found = true;
                        }
                        //stop
                        else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.stop)
                        {
                            found = true;
                        }
                        else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.plus && comp.ElementAt(i).COLOR == onboard.COLOR)
                        {
                            special = Takispecial.plus;
                            found = true;
                        }
                        else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.taki && comp.ElementAt(i).COLOR == onboard.COLOR)
                        {
                            special = Takispecial.taki;
                            found = true;
                        }
                        else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.none && comp.ElementAt(i).COLOR == onboard.COLOR)
                        {
                            special = Takispecial.none;
                            found = true;
                        }
                        else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.takiallcolors)
                        {
                            special = Takispecial.takiallcolors;
                            color = onboard.COLOR;
                            found = true;
                        }
                        else if (special == Takispecial.stop && comp.ElementAt(i).SPECIAL == Takispecial.changecolor)
                        {
                            special = Takispecial.changecolor;
                            found = true;
                        }
                        //taki
                        else if (special == Takispecial.taki && comp.ElementAt(i).SPECIAL == Takispecial.changecolor)
                        {
                            found = true;
                        }
                        else if (special == Takispecial.taki && comp.ElementAt(i).NUMBER == onboard.NUMBER && comp.ElementAt(i).COLOR != onboard.COLOR)
                        {
                            special = comp.ElementAt(i).SPECIAL;
                            found = true;
                        }
                        else if (special == Takispecial.taki && comp.ElementAt(i).SPECIAL == Takispecial.taki)
                        {
                            found = true;
                        }
                        else if (special == Takispecial.taki && comp.ElementAt(i).SPECIAL == Takispecial.takiallcolors)
                        {
                            special = Takispecial.takiallcolors;
                            color = onboard.COLOR;
                            found = true;
                        }
                        else if (special == Takispecial.taki && comp.ElementAt(i).COLOR == onboard.COLOR)
                        {
                            found = true;
                        }
                        //taki all colors
                        else if (special == Takispecial.takiallcolors && comp.ElementAt(i).SPECIAL == Takispecial.changecolor)
                        {
                            found = true;
                        }
                        else if (special == Takispecial.takiallcolors && comp.ElementAt(i).NUMBER == onboard.NUMBER && comp.ElementAt(i).COLOR != color)
                        {
                            special = comp.ElementAt(i).SPECIAL;
                            found = true;
                        }
                        else if (special == Takispecial.takiallcolors && comp.ElementAt(i).SPECIAL == Takispecial.taki && onboard.SPECIAL == Takispecial.takiallcolors)
                        {
                            special = Takispecial.taki;
                            found = true;
                        }
                        else if (special == Takispecial.takiallcolors && comp.ElementAt(i).SPECIAL == Takispecial.taki && onboard.SPECIAL != Takispecial.takiallcolors && onboard.COLOR == comp.ElementAt(i).COLOR)
                        {
                            special = Takispecial.taki;
                            found = true;
                        }
                        else if (special == Takispecial.takiallcolors && comp.ElementAt(i).COLOR == color)
                        {
                            found = true;
                        }
                        // change color
                        else if (special == Takispecial.changecolor && (comp.ElementAt(i).SPECIAL == Takispecial.changecolor || comp.ElementAt(i).SPECIAL == Takispecial.takiallcolors))
                        {
                            special = comp.ElementAt(i).SPECIAL;
                            color = onboard.COLOR;
                            found = true;
                        }

                        else if (special == Takispecial.changecolor && (comp.ElementAt(i).COLOR == onboard.COLOR || comp.ElementAt(i).NUMBER == onboard.NUMBER))
                        {
                            special = comp.ElementAt(i).SPECIAL;
                            found = true;
                        }
                        else //not valid
                        {
                            i++;
                        }
                    }

                    if (found)
                    {
                        timesofnotplay = 0;
                        onboard = comp.ElementAt(i);
                        onboard.SetLocation(600, 400);
                        onboard.TURNED = false;
                        comp.Remove(comp.ElementAt(i));
                        if (special == Takispecial.takiallcolors)
                        {
                            onboard.COLOR = color;
                        }
                        else if (special == Takispecial.changecolor)
                        {

                            random_color = rnd.Next(1, 5);
                            switch (random_color)
                            {
                                case (1):
                                    color = Takicolor.red;
                                    break;
                                case (2):
                                    color = Takicolor.blue;
                                    break;
                                case (3):
                                    color = Takicolor.green;
                                    break;
                                case (4):
                                    color = Takicolor.yellow;
                                    break;
                            }

                            onboard.COLOR = color;
                            MessageBox.Show("the computer choose the color " + color);
                            turn = !turn;
                        }
                        else if (special == Takispecial.none)
                        {
                            turn = !turn;
                        }
                        if (special == Takispecial.taki || special == Takispecial.takiallcolors)
                        {
                            for (int j = 0; j < comp.Count && !check; j++)
                            {
                                if (comp.ElementAt(j).COLOR == onboard.COLOR || (onboard.SPECIAL != Takispecial.none && comp.ElementAt(j).SPECIAL == onboard.SPECIAL) ||
                                    (onboard.SPECIAL == Takispecial.none && comp.ElementAt(j).NUMBER == onboard.NUMBER && comp.ElementAt(j).COLOR != color) ||
                                    (onboard.SPECIAL == Takispecial.taki && comp.ElementAt(j).SPECIAL == Takispecial.takiallcolors) ||
                                    (onboard.SPECIAL == Takispecial.takiallcolors && comp.ElementAt(j).SPECIAL == Takispecial.taki) || (comp.ElementAt(j).SPECIAL == Takispecial.changecolor))
                                {
                                    counter++;
                                    check = !check;
                                }
                            }
                            if (counter == 0)// he dont have more cards to play
                            {
                                special = onboard.SPECIAL;
                                turn = !turn;
                                special = Takispecial.none;
                            }
                        }
                    }
                    else
                    {
                        if (comp.Count < 8)
                        {
                            comp.Add(takiDeck.Getcard);
                        }
                        else
                        {
                            timesofnotplay++;
                            turn = !turn;
                            special = Takispecial.none;
                        }
                    }
                    pictureBox19.Refresh();
                    if (comp.Count == 0)
                    {
                        victory = true;
                        MessageBox.Show(" computer won ");
                    }
                    if (timesofnotplay == 2)//both cant play so change the board
                    {
                        onboard = takiDeck.Getcard;
                        while (onboard.SPECIAL == Takispecial.changecolor || onboard.SPECIAL == Takispecial.takiallcolors)
                        {
                            onboard = takiDeck.Getcard;
                        }
                        onboard.TURNED = false;
                        onboard.SetLocation(600, 400);
                        timesofnotplay = 0;
                    }

                }
            }
            
        }
    }
}
