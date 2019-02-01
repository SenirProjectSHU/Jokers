using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeckForGame
{
    class HandEval
    {
        //Input: Array of cards
        //  ARRAY INPUTTED SHOULD BE A THROW AWAY AS THE ARRAY WILL BE SORTED
        //Output: Results
        //  Index 0 takes highest priority in evaluation. Subsequent numbers are for tie breaking.
        //  In the case of a flush, Index 0 is 5. If there needs to be a tie break, compare 
        //  at index 1. If a tie break is needed, compare index 2, and so forth. 
        //  If the result array is equal to another result array, the pot is split.
        public byte[] Evaluate(Card[] hand)
        {
            //Init resources
            //chain_val refers to highest value in chain
            byte clubs = 0, hearts = 0, spades = 0, diamonds = 0, //used for flush
                longest_chain = 1, chain_val = 0, current_chain = 1, //used for straight types
                highest_triple = 0, highest_pair = 0, //used for full house
                flush_tester = 0; //for testing straight flush types
            char flush_suit; //what suit the flush is in
            byte[] val_count = new byte[13]; //value 2 is index 0

            //Sort hand by value
            //QuickSort(hand, 0, (byte)(hand.Length - 1)); //Removed due to inconsistant results
            CocktailSort(hand);

            //Summary (longest chain)
            if (hand[0].MyValue == 2 && hand[hand.Length - 1].MyValue == 14) { current_chain = 2; }
            for (byte x = 1; x < hand.Length; x++)
            {
                if (hand[x].MyValue == hand[x - 1].MyValue) { continue; }
                if (hand[x].MyValue == hand[x - 1].MyValue + 1) { current_chain++; }
                else { current_chain = 1; }
                if (current_chain > longest_chain)
                {
                    longest_chain = current_chain;
                    chain_val = (byte)hand[x].MyValue;
                }
            }

            //Summary (count suits and values)
            for ( int x = 0; x < hand.Length; x++)
            {
                val_count[(int)hand[x].MyValue - 2]++;
                if (hand[x].MySuit == 'S') { spades++; continue; }
                if (hand[x].MySuit == 'H') { hearts++; continue; }
                if (hand[x].MySuit == 'C') { clubs++; continue; }
                if (hand[x].MySuit == 'D') { diamonds++; continue; }
            }

            //Summary (determine flush suit)
            if (clubs >= 5) { flush_suit = 'C'; }
            else if (diamonds >= 5) { flush_suit = 'D'; }
            else if (spades >= 5) { flush_suit = 'S'; }
            else if (hearts >= 5) { flush_suit = 'H'; }
            else { flush_suit = 'X'; }

            //Summary (determine highest pair and triple)
            for (int x = val_count.Length - 1; x >= 0; x--)
            {
                if (val_count[x] == 3 && highest_triple == 0)
                {
                    highest_triple = (byte)(x + 2);
                }
                else if (val_count[x] == 2 && highest_pair == 0)
                {
                    highest_pair = (byte)(x + 2);
                }
            }

            //Test in order of value (descending)
            //Return when test positive

            //Royal flush
            if (longest_chain >= 5 && chain_val == 14 && flush_suit != 'X')
            {
                for (int x = (hand.Length - 1); x >= 0; x--)
                {
                    if (hand[x].MyValue >= 10)
                    {
                        if (hand[x].MySuit == flush_suit)
                        {
                            flush_tester++;
                        }
                    }
                    else break;
                }
                if (flush_tester == 5)
                {
                    return new byte[] { 9, 14 };
                }
                flush_tester = 0;
            }

            //Straight flush
            if(longest_chain >= 5 && flush_suit != 'X' && hand.Length > 4)
            {
                bool[] sf_check = new bool[15];
                byte sf_value = 0;
                for(int i = 0; i < sf_check.Length; i++) { sf_check[i] = false; }
                for(int i = 0; i < hand.Length; i++)
                {
                    if(hand[i].MySuit == flush_suit)
                    {
                        if(hand[i].MyValue == 14)
                        {
                            sf_check[1] = true;
                        }
                        sf_check[(int)hand[i].MyValue] = true;
                    }
                }
                for(int i = sf_check.Length-1; i > 0; i--)
                {
                    if (sf_check[i])
                    {
                        if(i == sf_check.Length - 1 || !sf_check[i + 1])
                        {
                            sf_value = (byte)i;
                        }
                        flush_tester++;
                        if(flush_tester == 5)
                        {
                            return new byte[] { 8, sf_value };
                        }
                    }
                    else
                    {
                        flush_tester = 0;
                    }
                }
            }

            //Four of a kind
            for (int x = val_count.Length-1; x >= 0; x--)
            {
                if (val_count[x] == 4)
                {
                    return new byte[] { 7, (byte)(x + 2) };
                }
            }

            //Full house (note: index 1 is value of triple. index 2 is value of pair)
            if(highest_triple != 0 && highest_pair != 0)
            {
                return new byte[] { 6, highest_triple, highest_pair };
            }

            //Flush (card value is complicated)
            if ( flush_suit != 'X')
            {
                byte[] flush_card = new byte[5];
                byte i = 0;
                for (int x = hand.Length - 1; x >= 0; x--)
                {
                    if(hand[x].MySuit == flush_suit)
                    {
                        flush_card[i++] = hand[x].MyValue;
                        if(i == 5)
                        {
                            return new byte[] { 5, flush_card[0], flush_card[1],
                                flush_card[2], flush_card[3], flush_card[4] };
                        }
                    }
                }
            }

            //Straight
            if(longest_chain >= 5)
            {
                return new byte[] { 4, chain_val };
            }

            //Three of a kind
            if(highest_triple != 0)
            {
                return new byte[] { 3, highest_triple };
            }

            //Two pair and Pair
            if (highest_pair != 0)
            {
                for (int x = val_count.Length - 1; x >= 0; x--)
                {
                    if (val_count[x] >= 2 && x < highest_pair - 2)
                    {
                        return new byte[] { 2, highest_pair, (byte)(x + 2) };
                    }
                }

                return new byte[] { 1, highest_pair };
            }

            //High Card
            return new byte[] { 0, (byte)hand[hand.Length-1].MyValue };
        }

        private static void QuickSort(Card[] arr, byte left, byte right)
        {
            if(left < right)
            {
                byte pivot = Partition(arr, left, right);

                if (pivot > 1)
                {
                    QuickSort(arr, left, (byte)(pivot-1));
                }
                if (pivot + 1 < right)
                {
                    QuickSort(arr, (byte)(pivot+1), right);
                }
            }
        }

        private static byte Partition(Card[] arr, byte left, byte right)
        {
            Card pivot = arr[left];
            while (true)
            {
                while (arr[left].MyValue < pivot.MyValue)
                {
                    left++;
                }

                while (arr[right].MyValue > pivot.MyValue)
                {
                    right--;
                }

                if (left < right)
                {
                    if (arr[left].MyValue == arr[right].MyValue) return right;

                    Card temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
                else
                {
                    return right;
                }
            }
        }

        private static void CocktailSort(Card[] A)
        {
            bool swapped;
            Card temp;
            do
            {
                swapped = false;
                for (int i = 0; i <= A.Length - 2; i++)
                {
                    if (A[i].MyValue > A[i + 1].MyValue)
                    {
                        //test whether the two elements are in the wrong order
                        temp = A[i];
                        A[i] = A[i + 1];
                        A[i + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped)
                {
                    //we can exit the outer loop here if no swaps occurred.
                    break;
                }
                swapped = false;
                for (int i = A.Length - 2; i >= 0; i--)
                {
                    if (A[i].MyValue > A[i + 1].MyValue)
                    {
                        temp = A[i];
                        A[i] = A[i + 1];
                        A[i + 1] = temp;
                        swapped = true;
                    }
                }
                //if no elements have been swapped, then the list is sorted
            } while (swapped);
        }

    }
}
