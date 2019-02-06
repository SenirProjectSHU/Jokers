using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holdem
{
    /// <summary>
    /// standard fair deck of 52 cards
    /// </summary>
    public class Deck
    {
        private List<Card> deck = new List<Card>();
        public Deck()
        {
            deck.Add(new Card(14, 1));
            for (byte i = 2; i <= 13; i++)
            {
                deck.Add(new Card(i, 1));
            }
            deck.Add(new Card(14, 2));
            for (byte i = 2; i <= 13; i++)
            {
                deck.Add(new Card(i, 2));
            }
            for (byte i = 13; i >= 2; i--)
            {
                deck.Add(new Card(i, 3));
            }
            deck.Add(new Card(14, 3));
            for (byte i = 13; i >= 2; i--)
            {
                deck.Add(new Card(i, 4));
            }
            deck.Add(new Card(14, 4));
        }
        public Deck(bool faceUp)
        {
            deck.Add(new Card(14, 1, faceUp));
            for (byte i = 2; i <= 13; i++)
            {
                deck.Add(new Card(i, 1, faceUp));
            }
            deck.Add(new Card(14, 2, faceUp));
            for (byte i = 2; i <= 13; i++)
            {
                deck.Add(new Card(i, 2, faceUp));
            }
            for (byte i = 13; i >= 2; i--)
            {
                deck.Add(new Card(i, 3, faceUp));
            }
            deck.Add(new Card(14, 3, faceUp));
            for (byte i = 13; i >= 2; i--)
            {
                deck.Add(new Card(i, 4, faceUp));
            }
            deck.Add(new Card(14, 4, faceUp));
        }
        public Deck(Deck otherDeck)
        {
            foreach (Card card in otherDeck.deck)
            {
                this.deck.Add(new Card(card));
            }
        }
        public void Add(Card card)
        {
            deck.Add(card);
        }
        public int CardsLeft()
        {
            return deck.Count;
        }
        //<SHUFFLING>
        public void Shuffle()
        {
            var rand = new Random();
            for (int i = CardsLeft() - 1; i > 0; i--)
            {
                int n = rand.Next(i + 1);
                Card temp = deck[i];
                deck[i] = deck[n];
                deck[n] = temp;
            }
        }
        public void PerfectCut()
        {
            Card c;
            int x = (int)Math.Floor((double)deck.Count / 2.0);
            for (int i = 1; i <= x; i++)
            {
                c = deck[deck.Count - 1];
                deck.RemoveAt(deck.Count - 1);
                deck.Insert(0, c);
            }
        }
        //human cut
        public void Cut()
        {
            Card c;
            int x = 0;
            Random r = new Random();
            for (int i = 1; i <= deck.Count; i++) { x += r.Next(2); }
            for (int i = 1; i <= x; i++)
            {
                c = deck[deck.Count - 1];
                deck.RemoveAt(deck.Count - 1);
                deck.Insert(0, c);
            }
        }
        //perfect riffle
        public void PerfectRiffle(bool preserveEnds)
        {
            List<Card> temp1 = new List<Card>();
            List<Card> temp2 = new List<Card>();
            int x = (int)Math.Floor((double)deck.Count / 2.0);
            int size = deck.Count();
            for (int i = 1; i <= x; i++)
            {
                temp1.Insert(0, deck[deck.Count - 1]);
                deck.RemoveAt(deck.Count - 1);
            }
            for (int i = 1; i <= x; i++)
            {
                temp2.Insert(0, deck[deck.Count - 1]);
                deck.RemoveAt(deck.Count - 1);
            }
            for (int i = 1; i <= size; i++)
            {
                if (preserveEnds)
                {
                    deck.Insert(0, temp1[temp1.Count() - 1]);
                    temp1.RemoveAt(temp1.Count() - 1);
                }
                else
                {
                    deck.Insert(0, temp2[temp2.Count() - 1]);
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
            int x, size = deck.Count;
            Random r = new Random();

            //finds where to split
            if (perfectCut)
            {
                x = (int)Math.Floor((double)deck.Count / 2.0);
            }
            else
            {
                x = 0;
                for (int i = 1; i <= deck.Count; i++) { x += r.Next(2); }
            }

            //splits deck
            for (int i = size - 1; i >= x; i--)
            {
                temp1.Insert(0, deck[i]);
                deck.RemoveAt(i);
            }
            for (int i = x - 1; i >= 0; i--)
            {
                temp2.Insert(0, deck[i]);
                deck.RemoveAt(i);
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
                    deck.Insert(0, temp1[temp1.Count() - 1]);
                    temp1.RemoveAt(temp1.Count() - 1);
                }
                else
                {
                    if (temp2.Count() == 0)
                    {
                        goto finish1;
                    }
                    deck.Insert(0, temp2[temp2.Count() - 1]);
                    temp2.RemoveAt(temp2.Count() - 1);
                }
            }

            //if imperfect split
            finish1:
            for (int i = temp1.Count() - 1; i >= 0; i--)
            {
                deck.Insert(0, temp1[i]);
                temp1.RemoveAt(i);
            }
            return;
            finish2:
            for (int i = temp2.Count() - 1; i >= 0; i--)
            {
                deck.Insert(0, temp2[i]);
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
            while (deck.Count() > 0)
            {
                //select random number
                x = 11; //offset
                for (int i = 0; i < 14; i++) { x += r.Next(2); }
                if (x > deck.Count() - 1) { x = deck.Count() - 1; } //prevent overflow

                //create strip and put in temp
                for (int i = x; i >= 0; i--)
                {
                    temp.Insert(0, deck[i]);
                    deck.RemoveAt(i);
                }
            }

            //put temp into deck
            while (temp.Count() != 0)
            {
                deck.Add(temp[0]);
                temp.RemoveAt(0);
            }
            return;
        }
        //</SHUFFLING>
        //<MARKING>
        public void Mark(Card c, char mark)
        {
            deck.Find(x => x.Equals(c)).setMark(mark);
        }
        public void Mark(byte rank, byte suit, char mark)
        {
            deck.Find(x => x.Equals(new Card(rank, suit))).setMark(mark);
        }
        public void Mark(RANK rank, SUIT suit, char mark)
        {
            deck.Find(x => x.Equals(new Card(rank, suit))).setMark(mark);
        }
        //</MARKING>
        //<PRINTING>
        public string Print()
        {
            string output = "";
            foreach (Card card in deck)
            {
                output += card.ToString() + " ";
            }
            return output;
        }
        //</PRINTING>
        //<DEALING CARDS>
        public Card Deal()
        {
            Card c = deck[0];
            deck.RemoveAt(0);
            c.FaceUp = true;
            return c;
        }
        public Card Deal(bool faceUp)
        {
            Card c = deck[0];
            deck.RemoveAt(0);
            c.FaceUp = faceUp;
            return c;
        }
        public Card SecondDeal()
        {
            Card c = deck[1];
            deck.RemoveAt(1);
            c.FaceUp = true;
            return c;
        }
        public Card SecondDeal(bool faceUp)
        {
            Card c = deck[1];
            deck.RemoveAt(1);
            c.FaceUp = faceUp;
            return c;
        }
        public Card BottomDeal()
        {
            Card c = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            c.FaceUp = true;
            return c;
        }
        public Card BottomDeal(bool faceUp)
        {
            Card c = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            c.FaceUp = faceUp;
            return c;
        }
        //</DEALING CARDS>

        public void Remove(int index)
        {
            if (index < 0 || index >= deck.Count)
                throw new ArgumentOutOfRangeException();
            deck.RemoveAt(index);
        }
        public void Remove(Card card)
        {
            for(int i=0;i<deck.Count;i++)
            {
                if (deck[i] == card && deck[i].getSuit() == card.getSuit())
                {
                    deck.RemoveAt(i);
                }
            }
        }
        public Card[] ToArray()
        {
            return deck.ToArray();
        }
        public List<Card> ToList()
        {
            return deck;
        }
    }
}
