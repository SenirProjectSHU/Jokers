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
            deck.PrintAll();
            Console.WriteLine("==========================");
            deck.Mark(14, 'S', 'P');
            deck.Mark(13, 'S', 'P');
            deck.Mark(11, 'S', 'T');
            deck.Mark(9, 'C', 'T');
            deck.PrintMarked(true);
            Console.WriteLine("==========================");
            Console.ReadLine();
        }
    }
}
