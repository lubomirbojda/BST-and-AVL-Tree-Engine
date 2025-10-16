using System.Diagnostics;
using AUS2_S_1.Trees.AVLTree;
using AUS2_S_1.Trees.BinarySearchTree;

namespace AUS2_S_1
{
    internal class Program
    {
        static void Main()
        {
            //int[] numbers = [8, 3, 10, 1, 6, 14, 4, 7, 13];
            //int[] numbers = [32,16,48,8,24,40,56,4,12,20,28,36,44,52,60,2,6,10,14,18,22,26,30,34,38,42,46,50,54,58,62, 1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33,35,37,39,41,43,45,47,49,51,53,55,57,59,61,63];

            //BST<int> bstTest = new();
            //bstTest.Insert();

            //for (int i = 0; i < numbers.Length; i++)
            //{
            //    bstTest.Insert(numbers[i]);
            //}

            //var searchTest = bstTest.Search(6);
            //if (searchTest != null)
            //    Console.WriteLine(searchTest.Data.ToString());

            //bstTest.Delete(13);
            //bstTest.Delete(14);
            //bstTest.Delete(3);
            //bstTest.Delete(14);

            Console.WriteLine("DONE");
            //var min = bstTest.FindMinimum(bstTest.Root);
            //Console.WriteLine(min.Data.ToString());

            int x = 10_000_000;
            Random r = new();
            Random rS = new();
            AVLT<int> avltTest = new();
            //BST<int> avltTest = new();
            Stopwatch swInsert = new Stopwatch();
            List<int> inserted = new List<int>();
            Stopwatch swSearch = new Stopwatch();

            int insertedCount = 0;
            int sameInts = 0;

            for (int i = 0; i < x; i++)
            {
                var rr = r.Next();

                swInsert.Start();
                insertedCount += avltTest.Insert(rr) ? 1 : 0;
                swInsert.Stop();

                //if (inserted.Contains(rr))
                //{
                //    sameInts++;
                //}
                //else
                //{
                //    inserted.Add(rr);
                //}
                inserted.Add(rr);
            }

            int deletedCount = 0;
            int y = 2_000_000;
            for (int i = 0; i < y; i++)
            {
                var rr = rS.Next(0, inserted.Count);
                var toFind = inserted[rr];

                swSearch.Start();
                deletedCount += avltTest.Delete(toFind) ? 1 : 0;
                swSearch.Stop();


                //inserted.RemoveAt(rr);
                //inserted.Add(rr);
            }

            var avgInsert = swInsert.ElapsedMilliseconds / (double)x;
            var avgSearch = swSearch.ElapsedMilliseconds / (double)y;
            Console.WriteLine("Insert 10_000_000 datas:");
            Console.WriteLine("Inserted: " + insertedCount);
            
            Console.WriteLine("average (ms): " + avgInsert  + "\n" + "full-time (s): " + swInsert.ElapsedMilliseconds / (double)1000);
            Console.WriteLine();
            Console.WriteLine("Finding 5_000_000 datas:");
            Console.WriteLine("average (ms): " + avgSearch + "\n" + "full-time (s): " + swSearch.ElapsedMilliseconds / (double)1000);

            Console.WriteLine("deleted: " + deletedCount);

            //foreach (int number in numbers)
            //{
            //    avltTest.Insert(number);
            //}

            //foreach (var n in avltTest.FindInterval(32, 32))
            //{
            //    Console.Write(n + "-> ");
            //}

            //avltTest.Delete(32);
            //avltTest.RightRotation(avltTest.Search(15));

            //Console.WriteLine("ajal");
        }
    }
}
