using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckForGame
{
    class Program
    {   
        static void Main(string[] args)
        {
            DeckOfCards deck = new DeckOfCards();
            foreach(Card c in deck.myDeck){
                Console.WriteLine(c.toString());
            }
            Console.WriteLine("==========================");
            deck.Riffle(true);
            Console.WriteLine("==========================");
            foreach (Card c in deck.myDeck)
            {
                Console.WriteLine(c.toString());
            }
            Console.ReadLine();
        }
    }
}
