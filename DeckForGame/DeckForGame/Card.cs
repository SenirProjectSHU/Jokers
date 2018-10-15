using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DeckForGame
{
    enum SUIT { HEARTS=1, CLUBS=2, DIAMONDS=3, SPADES=4 };
    enum VALUE { 
        TWO=2, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT,
        NINE, TEN, JACK, QUEEN, KING, ACE
    };
    class Card
    {
        private char mySuit { get; set; }
        private int myValue { get; set; }
        public Card(char s, int v)
        {
            mySuit = s; //should accept H C D S
            myValue = v; //should accept 2-14
        }
        public void setCard(char s, int v)
        {
            mySuit = s; //should accept H C D S
            myValue = v; //should accept 2-14
        }
        public string toString()
        {
            char[] chars = { '?', mySuit };
            switch(myValue)
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
                    chars[0] = 'X';
                    break;
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
            return new string(chars);
        }
    }
}
