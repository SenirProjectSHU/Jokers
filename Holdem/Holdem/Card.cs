using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Resources;
namespace Holdem
{
    public enum RANK
    {
        TWO=2, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN, JACK, QUEEN, KING, ACE
    }
    public enum SUIT
    {
        HEARTS = 1,
        CLUBS,
        DIAMONDS,
        SPADES
    }
    /// <summary>
    /// THIS IS THE CLASS THAT STARTED EVERYTHING AND MADE THIS GAME POSSIBLE
    /// </summary>
    public class Card
    {
        private int rank, suit;
        private char mark; //what state the card is marked in.
        private Bitmap Image;
        private bool faceUp; //whether or not the card is actually face up
        private bool highlight;
        public bool FaceUp
        {
            get { return faceUp; }
            set 
            { 
                faceUp = value;
                getImageFromFile();
            }
        }
        //default two of diamonds
        public Card()
        {
            rank = (int)RANK.TWO;
            suit = (int)SUIT.DIAMONDS;
            this.mark = 'N';
            faceUp = false;
            highlight = false;
        }
        public Card(RANK rank,SUIT suit)
        {
            this.rank = (int)rank;
            this.suit = (int)suit;
            this.mark = 'N';
            faceUp = false;
            highlight = false;
        }
        public Card(int rank, int suit)
        {
            if (rank < 1 || rank > 14 || suit < 1 || suit > 4)
                throw new ArgumentOutOfRangeException();
            this.rank=rank;
            this.suit=suit;
            this.mark = 'N';
            faceUp =false;
            highlight = false;
        }
        public Card(RANK rank, SUIT suit,bool faceUp)
        {
            this.rank = (int)rank;
            this.suit = (int)suit;
            this.mark = 'N';
            this.faceUp = faceUp;
            highlight = false;
        }
        public Card(int rank, int suit,bool faceUp)
        {
            if (rank < 1 || rank > 14 || suit < 1 || suit > 4)
                throw new ArgumentOutOfRangeException();
            this.rank = rank;
            this.suit = suit;
            this.mark = 'N';
            this.faceUp = faceUp;
            highlight = false;
        }
        public Card(RANK rank, SUIT suit, char m)
        {
            this.rank = (int)rank;
            this.suit = (int)suit;
            setMark(m);
            faceUp = false;
            highlight = false;
        }
        public Card(int rank, int suit, char m)
        {
            if (rank < 1 || rank > 14 || suit < 1 || suit > 4)
                throw new ArgumentOutOfRangeException();
            this.rank = rank;
            this.suit = suit;
            setMark(m);
            faceUp = false;
            highlight = false;
        }
        public Card(RANK rank, SUIT suit, char m, bool faceUp)
        {
            this.rank = (int)rank;
            this.suit = (int)suit;
            setMark(m);
            this.faceUp = faceUp;
            highlight = false;
        }
        public Card(int rank, int suit, char m, bool faceUp)
        {
            if (rank < 1 || rank > 14 || suit < 1 || suit > 4)
                throw new ArgumentOutOfRangeException();
            this.rank = rank;
            this.suit = suit;
            setMark(m);
            this.faceUp = faceUp;
            highlight = false;
        }
        public Card(Card card)
        {
            this.rank = card.rank;
            this.suit = card.suit;
            this.mark = card.mark;
            this.faceUp = card.faceUp;
            highlight = false;
        }
        public static string rankToString(int rank)
        {
            switch (rank)
            {
                case 11:
                    return "Jack";
                case 12:
                    return "Queen";
                case 13:
                    return "King";
                case 14:
                    return "Ace";
                default:
                    return rank.ToString();
            }
        }
        public static string suitToString(int suit)
        {
            switch (suit)
            {
                case 1:
                    return "Hearts";
                case 2:
                    return "Clubs";
                case 3:
                    return "Diamonds";
                default:
                    return "Spades";
            }
        }
        public static char suitToChar(int suit)
        {
            switch (suit)
            {
                case 1:
                    return 'H';
                case 2:
                    return 'C';
                case 3:
                    return 'D';
                default:
                    return 'S';
            }
        }
        public int getRank()
        {
            return rank;
        }
        public int getSuit()
        {
            return suit;
        }
        //get image from depending on if the card is faceup or down
        private void getImageFromFile()
        {
            if (faceUp)
                this.Image = new Bitmap("Cards\\" + suit + "-" + rank + ".png");
            else
                this.Image = new Bitmap("Cards\\sb.bmp");
        }
        //get the current image
        public Bitmap getImage()
        {
            if(Image==null)
                getImageFromFile();
            return this.Image;
        }
        public void setRank(RANK rank)
        {
            this.rank = (int)rank;
        }
        public bool isMarked()
        {
            return (mark != 'N');
        }
        public void setMark(char m)
        {
            if(m == 'T' || m == 'P')
            {
                this.mark = m;
            }
            else
            {
                this.mark = 'N';
            }
        }
        public char getMark()
        {
            return mark;
        }
        public void setCard(RANK rank, SUIT suit)
        {
            this.rank = (int)rank;
            this.suit = (int)suit;
        }
        public void setCard(int rank, int suit)
        {
            if(rank<1||rank>14||suit<1||suit>4)
                throw new ArgumentOutOfRangeException();
            this.rank=rank;
            this.suit=suit;
        }
        public override string ToString()
        {
            if (faceUp == true)
                return rankToString(rank) + " of " + suitToString(suit);
            return "The card is facedown, you cannot see it!";
        }
        
        //extract green channel from image to highlight image
        public void Highlight()
        {
            if (faceUp == false)
                return;

            this.highlight = true;

            if (this.Image == null)
                getImageFromFile();
            Bitmap HighlightedBitmap = new Bitmap(Image.Width, Image.Height);
            for (int i = 0; i < Image.Width; i++)
            {
                for (int j = 0; j < Image.Height; j++)
                {
                    int green = Image.GetPixel(i, j).G;
                    HighlightedBitmap.SetPixel(i, j, Color.FromArgb(255, 0, green, 0));
                }
            }
            Image = new Bitmap(HighlightedBitmap);
        }
        //reload original image to unhighlight
        public void UnHighlight()
        {
            if (faceUp == false)
                return;
            this.highlight = false;
            getImageFromFile();
        }
        public bool isHighlighted()
        {
            return this.highlight;
        }

        //compare rank of cards
        public static bool operator ==(Card a, Card b)
        {
            if (a.rank == b.rank)
                return true;
            else
                return false;
        }
        public static bool operator !=(Card a, Card b)
        {
            if (a.rank != b.rank)
                return true;
            else
                return false;
        }
        public static bool operator <(Card a, Card b)
        {
            if (a.rank < b.rank)
                return true;
            else
                return false;
        }
        public static bool operator >(Card a, Card b)
        {
            if (a.rank > b.rank)
                return true;
            else
                return false;
        }
        public static bool operator <=(Card a, Card b)
        {
            if (a.rank <= b.rank)
                return true;
            else
                return false;
        }
        public static bool operator >=(Card a, Card b)
        {
            if (a.rank >= b.rank)
                return true;
            else
                return false;
        }
    }
    
}
