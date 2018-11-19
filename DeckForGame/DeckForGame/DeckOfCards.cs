using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DeckForGame
{
    class DeckOfCards
    {
        public List<Card> MyDeck = new List<Card>();

        //New Deck Order (Suits H C D S going A-K A-K K-A K-A)
        //All cards are not marked
        public DeckOfCards()
        {
            Card c = new Card(14, 'H');
            MyDeck.Add(c);
            for (byte i = 2; i <= 13; i++)
            {
                c = new Card(i, 'H');
                MyDeck.Add(c);
            }
            c = new Card(14, 'C');
            MyDeck.Add(c);
            for (byte i = 2; i <= 13; i++)
            {
                c = new Card(i, 'C');
                MyDeck.Add(c);
            }
            for (byte i = 13; i >= 2; i--)
            {
                c = new Card(i, 'D');
                MyDeck.Add(c);
            }
            c = new Card(14, 'D');
            MyDeck.Add(c);
            for (byte i = 13; i >= 2; i--)
            {
                c = new Card(i, 'S');
                MyDeck.Add(c);
            }
            c = new Card(14, 'S');
            MyDeck.Add(c);
        }

        //=====DEALING CARDS=====
        //deal top
        public Card Deal()
        {
            Card c = MyDeck[0];
            MyDeck.RemoveAt(0);
            return c;
        }

        //second deal
        public Card SecondDeal()
        {
            Card c = MyDeck[1];
            MyDeck.RemoveAt(1);
            return c;
        }

        //bottom deal
        public Card BottomDeal()
        {
            Card c = MyDeck[MyDeck.Count - 1];
            MyDeck.RemoveAt(MyDeck.Count - 1);
            return c;
        }

        //=====SHUFFLING=====
        //true shuffle
        public void Shuffle()
        {
            Random r = new Random();
            Card temp;
            int n = MyDeck.Count;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                temp = MyDeck[k];
                MyDeck[k] = MyDeck[n];
                MyDeck[n] = temp;
            }
        }
        
        //perfect cut
        public void PerfectCut()
        {
            Card c;
            int x = (int)Math.Floor((double)MyDeck.Count / 2.0);
            for (int i = 1; i <= x; i++)
            {
                c = MyDeck[MyDeck.Count - 1];
                MyDeck.RemoveAt(MyDeck.Count - 1);
                MyDeck.Insert(0, c);
            }
        }
        
        //human cut
        public void Cut()
        {
            Card c;
            int x = 0;
            Random r = new Random();
            for (int i = 1; i <= MyDeck.Count; i++) { x += r.Next(2); }
            for (int i = 1; i <= x; i++)
            {
                c = MyDeck[MyDeck.Count - 1];
                MyDeck.RemoveAt(MyDeck.Count - 1);
                MyDeck.Insert(0, c);
            }
        }

        //perfect riffle
        public void PerfectRiffle(bool preserveEnds)
        {
            List<Card> temp1 = new List<Card>();
            List<Card> temp2 = new List<Card>();
            int x = (int)Math.Floor((double)MyDeck.Count / 2.0);
            int size = MyDeck.Count();
            for (int i = 1; i <= x; i++)
            {
                temp1.Insert(0, MyDeck[MyDeck.Count - 1]);
                MyDeck.RemoveAt(MyDeck.Count - 1);
            }
            for (int i = 1; i <= x; i++)
            {
                temp2.Insert(0, MyDeck[MyDeck.Count - 1]);
                MyDeck.RemoveAt(MyDeck.Count - 1);
            }
            for (int i = 1; i <= size; i++)
            {
                if (preserveEnds)
                {
                    MyDeck.Insert(0, temp1[temp1.Count() - 1]);
                    temp1.RemoveAt(temp1.Count() - 1);
                }
                else
                {
                    MyDeck.Insert(0, temp2[temp2.Count() - 1]);
                    temp2.RemoveAt(temp2.Count() - 1);
                }
                preserveEnds = !preserveEnds;
            }
        }

        //random riffle
        public void Riffle(bool perfectCut)
        {
            //resources
            List<Card> temp1 = new List<Card>();
            List<Card> temp2 = new List<Card>();
            int x, size = MyDeck.Count;
            Random r = new Random();

            //finds where to split
            if (perfectCut)
            {
                x = (int)Math.Floor((double)MyDeck.Count / 2.0);
            }
            else
            {
                x = 0;
                for (int i = 1; i <= MyDeck.Count; i++) { x += r.Next(2); }
            }

            //splits deck
            for (int i = size - 1; i >= x; i--)
            {
                temp1.Insert(0, MyDeck[i]);
                MyDeck.RemoveAt(i);
            }
            for (int i = x - 1; i >= 0; i-- )
            {
                temp2.Insert(0, MyDeck[i]);
                MyDeck.RemoveAt(i);
            }

            //riffle
            for (int i = 1; i <= size; i++)
            {
                if (r.Next(2) == 0)
                {
                    if (temp1.Count() == 0)
                    {
                        goto finish2;
                    }
                    MyDeck.Insert(0, temp1[temp1.Count() - 1]);
                    temp1.RemoveAt(temp1.Count() - 1);
                }
                else
                {
                    if (temp2.Count() == 0)
                    {
                        goto finish1;
                    }
                    MyDeck.Insert(0, temp2[temp2.Count() - 1]);
                    temp2.RemoveAt(temp2.Count() - 1);
                }
            }

            //if imperfect split
            finish1:
            for (int i = temp1.Count() - 1; i >= 0; i--)
            {
                MyDeck.Insert(0, temp1[i]);
                temp1.RemoveAt(i);
            }
            return;
            finish2:
            for (int i = temp2.Count() - 1; i >= 0; i--)
            {
                MyDeck.Insert(0, temp2[i]);
                temp2.RemoveAt(i);
            }
            return;
        }

        public void Strip()
        {
            //resources
            List<Card> temp = new List<Card>();
            Random r = new Random();
            int x;

            //strip all cards
            while (MyDeck.Count() > 0)
            {
                //select random number
                x = 11; //offset
                for (int i = 0; i < 14; i++) { x += r.Next(2); }
                if (x > MyDeck.Count() - 1) { x = MyDeck.Count() - 1; } //prevent overflow

                //create strip and put in temp
                for (int i = x; i >= 0; i--)
                {
                    temp.Insert(0, MyDeck[i]);
                    MyDeck.RemoveAt(i);
                }
            }

            //put temp into myDeck
            while(temp.Count() != 0)
            {
                MyDeck.Add(temp[0]);
                temp.RemoveAt(0);
            }
            return;
        }

        //=====MARKING=====
        public void Mark(Card c, char m)
        {
            MyDeck.Find(x => x.Equals(c)).Marked = m;
        }

        public void Mark(byte v, char s, char m)
        {
            Card c = new Card(v, s);
            MyDeck.Find(x => x.Equals(c)).Marked = m;
        }

        //=====PRINTING=====
        public void PrintAll()
        {
            foreach (Card c in MyDeck)
            {
                Console.WriteLine(c.ToString());
            }
        }

        public void PrintMarked(bool ShowGapSize)
        {
            byte x = 0;
            foreach (Card c in MyDeck)
            {
                if (c.Marked == 'N')
                {
                    x++;
                }
                else
                {
                    if (x == 1)
                    {
                        Console.WriteLine("--");
                    }
                    else if (x > 1)
                    {
                        if (ShowGapSize) Console.WriteLine("=={0}", x);
                        else Console.WriteLine("==");
                    }
                    Console.WriteLine(c.ToString());
                    x = 0;
                }
            }
            if (x == 1)
            {
                Console.WriteLine("--");
            }
            else if (x > 1)
            {
                if (ShowGapSize) Console.WriteLine("=={0}", x);
                else Console.WriteLine("==");
            }
        }
    }
}
