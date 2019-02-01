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
            HandEval eval = new HandEval();
            deck.Shuffle();
            int size=7;
            
            Card[] hand = new Card[size];

            for (int i = 0; i < size; i++)
            {
                hand[i] = deck.Deal();
            }

            //hand[0] = new Card(2,'S');
            //hand[1] = new Card(3,'S');
            //hand[2] = new Card(4,'S');
            //hand[3] = new Card(5,'S');
            //hand[4] = new Card(6,'C');
            //hand[5] = new Card(7,'S');
            //hand[6] = new Card(14,'S');

            byte[] results = eval.Evaluate(hand);
            foreach (Card c in hand)
            {
                Console.WriteLine(c.ToString());
            }
            foreach (byte b in results)
            {
                Console.WriteLine(b);
            }
            Console.ReadLine();
        }
    }
}