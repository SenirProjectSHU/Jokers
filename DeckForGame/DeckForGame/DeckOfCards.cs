using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DeckForGame
{
    class DeckOfCards
    {
        public List<Card> myDeck = new List<Card>();

        //New Deck Order (Suits H C D S going A-K A-K K-A K-A)
        public DeckOfCards()
        {
            Card c = new Card('H', 14);
            myDeck.Add(c);
            for (int i = 2; i <= 13; i++)
            {
                c = new Card('H', i);
                myDeck.Add(c);
            }
            c = new Card('C', 14);
            myDeck.Add(c);
            for (int i = 2; i <= 13; i++)
            {
                c = new Card('C', i);
                myDeck.Add(c);
            }
            for (int i = 13; i >= 2; i--)
            {
                c = new Card('D', i);
                myDeck.Add(c);
            }
            c = new Card('D', 14);
            myDeck.Add(c);
            for (int i = 13; i >= 2; i--)
            {
                c = new Card('S', i);
                myDeck.Add(c);
            }
            c = new Card('S', 14);
            myDeck.Add(c);
        }

        //deal top
        public Card Deal()
        {
            Card c = myDeck[0];
            myDeck.RemoveAt(0);
            return c;
        }

        //second deal
        public Card SecondDeal()
        {
            Card c = myDeck[1];
            myDeck.RemoveAt(1);
            return c;
        }

        //bottom deal
        public Card BottomDeal()
        {
            Card c = myDeck[myDeck.Count - 1];
            myDeck.RemoveAt(myDeck.Count - 1);
            return c;
        }

        //perfect cut
        public void PerfectCut()
        {
            Card c;
            int x = (int)Math.Floor((double)myDeck.Count / 2.0);
            for (int i = 1; i <= x; i++)
            {
                c = myDeck[myDeck.Count - 1];
                myDeck.RemoveAt(myDeck.Count - 1);
                myDeck.Insert(0, c);
            }
        }
        
        //human cut
        public void Cut()
        {
            Card c;
            int x = 0;
            Random r = new Random();
            for (int i = 1; i <= myDeck.Count; i++) { x += r.Next(2); }
            for (int i = 1; i <= x; i++)
            {
                c = myDeck[myDeck.Count - 1];
                myDeck.RemoveAt(myDeck.Count - 1);
                myDeck.Insert(0, c);
            }
        }

        //perfect riffle
        public void PerfectRiffle(bool preserveEnds)
        {
            List<Card> temp1 = new List<Card>();
            List<Card> temp2 = new List<Card>();
            int x = (int)Math.Floor((double)myDeck.Count / 2.0);
            int size = myDeck.Count();
            for (int i = 1; i <= x; i++)
            {
                temp1.Insert(0, myDeck[myDeck.Count - 1]);
                myDeck.RemoveAt(myDeck.Count - 1);
            }
            for (int i = 1; i <= x; i++)
            {
                temp2.Insert(0, myDeck[myDeck.Count - 1]);
                myDeck.RemoveAt(myDeck.Count - 1);
            }
            for (int i = 1; i <= size; i++)
            {
                if (preserveEnds)
                {
                    myDeck.Insert(0, temp1[temp1.Count() - 1]);
                    temp1.RemoveAt(temp1.Count() - 1);
                }
                else
                {
                    myDeck.Insert(0, temp2[temp2.Count() - 1]);
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
            int x, size = myDeck.Count;
            Random r = new Random();

            //finds where to split
            if (perfectCut)
            {
                x = (int)Math.Floor((double)myDeck.Count / 2.0);
            }
            else
            {
                x = 0;
                for (int i = 1; i <= myDeck.Count; i++) { x += r.Next(2); }
            }

            //splits deck
            for (int i = size - 1; i >= x; i--)
            {
                temp1.Insert(0, myDeck[i]);
                myDeck.RemoveAt(i);
            }
            for (int i = x - 1; i >= 0; i-- )
            {
                temp2.Insert(0, myDeck[i]);
                myDeck.RemoveAt(i);
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
                    myDeck.Insert(0, temp1[temp1.Count() - 1]);
                    temp1.RemoveAt(temp1.Count() - 1);
                }
                else
                {
                    if (temp2.Count() == 0)
                    {
                        goto finish1;
                    }
                    myDeck.Insert(0, temp2[temp2.Count() - 1]);
                    temp2.RemoveAt(temp2.Count() - 1);
                }
            }

            //if imperfect split
            finish1:
            for (int i = temp1.Count() - 1; i >= 0; i--)
            {
                myDeck.Insert(0, temp1[i]);
                temp1.RemoveAt(i);
            }
            return;
            finish2:
            for (int i = temp2.Count() - 1; i >= 0; i--)
            {
                myDeck.Insert(0, temp2[i]);
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
            while (myDeck.Count() > 0)
            {
                //select random number
                x = 11; //offset
                for (int i = 0; i < 14; i++) { x += r.Next(2); }
                if (x > myDeck.Count() - 1) { x = myDeck.Count() - 1; } //prevent overflow

                //create strip and put in temp
                for (int i = x; i >= 0; i--)
                {
                    temp.Insert(0, myDeck[i]);
                    myDeck.RemoveAt(i);
                }
            }

            //put temp into myDeck
            while(temp.Count() != 0)
            {
                myDeck.Add(temp[0]);
                temp.RemoveAt(0);
            }
            return;
        }
    }
}
