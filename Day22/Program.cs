using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Day22
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> deck1 = new Queue<int>(Utils.Utils.ReadLinesFromConsole().Skip(1).Select(int.Parse));
            Queue<int> deck2 = new Queue<int>(Utils.Utils.ReadLinesFromConsole().Skip(1).Select(int.Parse));

            Dictionary<(string, string), bool> matchResults = new Dictionary<(string, string), bool>();

            bool firstPlayerVictory = PlayGame(deck1, deck2, matchResults);

            long res = 0;
            if (firstPlayerVictory)
            {
                Console.WriteLine($"First player won with deck {string.Join(",", deck1.ToList())}");
                int cardNumber = deck1.Count;
                foreach (int i in deck1)
                {
                    res += i * cardNumber;
                    cardNumber--;
                }
            }
            else
            {
                Console.WriteLine($"Second player won with deck {string.Join(",", deck2.ToList())}");
                int cardNumber = deck2.Count;
                foreach (int i in deck2)
                {
                    res += i * cardNumber;
                    cardNumber--;
                }
            }
            Console.WriteLine($"Part 1 : {res}");
        }

        static string ToString(Queue<int> deck)
        {
            return string.Join(' ', deck);
        }

        static bool PlayGame(Queue<int> deck1, Queue<int> deck2, Dictionary<(string, string), bool> matchResult, int depth = 0)
        {
            //if (matchResult.ContainsKey((ToString(deck1), ToString(deck2))))
            //{
            //    return matchResult[""]
            //}
            //Console.WriteLine($"Starting game with depth {depth}".PadLeft(26 + depth));
            HashSet<(string, string)> memory = new HashSet<(string, string)>();
            while (deck1.Count > 0 && deck2.Count > 0)
            {
                if (memory.Contains((ToString(deck1), ToString(deck2))))
                {
                    return true;
                }
                memory.Add((ToString(deck1), ToString(deck2)));
                int card1 = deck1.Dequeue();
                int card2 = deck2.Dequeue();
                bool firstPlayerVictory;

                if (deck1.Count >= card1 && deck2.Count >= card2)
                {
                    // playing a sub game
                    Queue<int> subDeck1 = new Queue<int>(deck1.ToList().Take(card1));
                    Queue<int> subDeck2 = new Queue<int>(deck2.ToList().Take(card2));

                    firstPlayerVictory = PlayGame(subDeck1, subDeck2, matchResult, depth + 1);
                }
                else
                {
                    firstPlayerVictory = card1 > card2;
                }

                if (firstPlayerVictory)
                {
                    deck1.Enqueue(card1);
                    deck1.Enqueue(card2);
                }
                else
                {
                    deck2.Enqueue(card2);
                    deck2.Enqueue(card1);
                }
            }
            return deck1.Count > 0;
        }
    }
}
