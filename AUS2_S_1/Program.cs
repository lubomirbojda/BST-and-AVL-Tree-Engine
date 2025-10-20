using System.Diagnostics;
using AUS2_S_1.Tests;
using AUS2_S_1.Trees.AVLTree;
using AUS2_S_1.Trees.BinarySearchTree;

namespace AUS2_S_1
{
    internal class Program
    {
        static void Main()
        {
            var tester = new Tester();
            tester.TestInsert();
            //tester.TestDelete();


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

            //Console.WriteLine("DONE");
            //var min = bstTest.FindMinimum(bstTest.Root);
            //Console.WriteLine(min.Data.ToString());

            //Console.WriteLine("deleted: " + deletedCount);

            //AVLT<int> avlttest = new AVLT<int>();
            //foreach (int number in numbers)
            //{
            //    avlttest.Insert(number);
            //}

            //foreach (var x in avlttest.FindInterval(5, 13))
            //{
            //    Console.Write(x + "-> ");
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
