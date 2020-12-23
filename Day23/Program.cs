using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Day23
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList<int> liste = new LinkedList<int>(new List<int> { 8, 7, 1, 3, 6, 9, 4, 5, 2 });
            // LinkedList<int> liste = new LinkedList<int>(new List<int> { 3, 8, 9, 1, 2, 5, 4, 6, 7 });
            int N = 1000000;
            for (int i = 10; i <= N; i++)
            {
                liste.AddLast(i);
            }

            LinkedListNode<int> current = liste.First;
            var indexes = new Dictionary<int, LinkedListNode<int>>();
            var s = liste.First;
            while (s != null)
            {
                indexes.Add(s.Value, s);
                s = s.Next;
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();

            for (int i = 0; i < 10000000; i++)
            {
                LinkedListNode<int> node1 = current.Next ?? liste.First;
                LinkedListNode<int> node2 = node1.Next ?? liste.First;
                LinkedListNode<int> node3 = node2.Next ?? liste.First;

                // Finding next value
                int nextValue = current.Value - 1;
                while (nextValue == node1.Value || nextValue == node2.Value || nextValue == node3.Value || nextValue < 1)
                {
                    nextValue--;
                    if (nextValue <= 0)
                    {
                        nextValue = N;
                    }
                }

                // Moving the cups
                LinkedListNode<int> destination = indexes[nextValue];
                liste.Remove(node1);
                liste.Remove(node2);
                liste.Remove(node3);
                liste.AddAfter(destination, node3);
                liste.AddAfter(destination, node2);
                liste.AddAfter(destination, node1);

                // moving current
                current = current.Next ?? liste.First;
            }

            LinkedListNode<int> position = liste.Find(1);
            long answer = 1;
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(position.Value);
                answer *= (long)position.Value;
                position = position.Next ?? liste.First;
            }

            Console.WriteLine($"part 2 answer is {answer}");
            Console.WriteLine($"Elapsed : {watch.Elapsed}");
        }

        private static void Display(LinkedList<int> words, string test)
        {
            Console.WriteLine(test);
            foreach (int word in words)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
        }
    }
}
