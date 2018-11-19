using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckForGame
{
    class Table
    {
        int Pot;
        Card[] Burn = new Card[3];
        List<Card> Community = new List<Card>();

        public int SplitPot(byte SplitBetween)
        {
            if (SplitBetween <= 1)
            {
                return Pot;
            }
            else
            {
                int temp = Pot;
                Pot = Pot % SplitBetween;
                return (int)Math.Floor((double)temp / (double)SplitBetween);

            }
        }
    }
}
