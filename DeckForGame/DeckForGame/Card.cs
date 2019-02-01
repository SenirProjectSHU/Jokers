using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DeckForGame
{
    class Card
    {
        private char marked, suit;
        private byte val;

        //11, 12, 13, 14 = J, Q, K, A
        public byte MyValue
        {
            get => this.val;
            set => val = (value >= 2 && value <= 14) ? value : (byte)2;
        }

        //H = Hearts
        //C = Clubs
        //D = Diamonds
        //S = Spades
        public char MySuit
        {
            get => this.suit;
            set => suit = (value == 'H' || value == 'C' || value == 'D' || value == 'S') ? value : 'H';
        }

        //N = Not marked
        //P = Permanently marked
        //T = Temporarily marked
        public char Marked
        {
            get => this.marked;
            set => this.marked = (value == 'T' || value == 'P') ? value : 'N';
        }

        public Card(byte v, char s)
        {
            MySuit = s; //should accept H C D S
            MyValue = v; //should accept 2-14
            Marked = 'N';
        }
        public Card(byte v, char s, char m)
        {
            MySuit = s; //should accept H C D S
            MyValue = v; //should accept 2-14
            Marked = m; //should accept N P T
        }
        public void SetCard(byte v, char s)
        {
            MySuit = s; //should accept H C D S
            MyValue = v; //should accept 2-14
        }
        public override string ToString()
        {
            char[] chars = new char[3];
            switch(MyValue)
            {
                case 2:
                    chars[0] = '2';
                    break;
                case 3:
                    chars[0] = '3';
                    break;
                case 4:
                    chars[0] = '4';
                    break;
                case 5:
                    chars[0] = '5';
                    break;
                case 6:
                    chars[0] = '6';
                    break;
                case 7:
                    chars[0] = '7';
                    break;
                case 8:
                    chars[0] = '8';
                    break;
                case 9:
                    chars[0] = '9';
                    break;
                case 10:
                    chars[0] = '1';
                    chars[1] = '0';
                    chars[2] = MySuit;
                    return new string(chars);
                case 11:
                    chars[0] = 'J';
                    break;
                case 12:
                    chars[0] = 'Q';
                    break;
                case 13:
                    chars[0] = 'K';
                    break;
                case 14:
                    chars[0] = 'A';
                    break;
                default:
                    break;
            }
            chars[1] = MySuit;
            return new string(chars);
        }

        public bool Equals(Card c)
        {
            return (this.MyValue == c.MyValue && this.MySuit == c.MySuit) ? true : false;
        }
    }
}