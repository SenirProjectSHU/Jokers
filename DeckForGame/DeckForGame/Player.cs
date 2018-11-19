using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckForGame
{
    class Player
    {
        private Card[] Hand = new Card[2];
        private int Wallet;

        public Player(int WalletInput)
        {
            Wallet = WalletInput;
            Hand[0] = Hand[1] = null;
        }

        public Card[] GetHand()
        {
            return Hand;
        }

        public Card[] CollectHand()
        {
            Card[] temp = Hand;
            Hand[0] = null;
            Hand[1] = null;
            return temp;
        }
    }
}
