using AUS2_S_1.Trees.AVLTree;
using AUS2_S_1.Trees.BinarySearchTree;

namespace AUS2_S_1
{
    internal class Program
    {
        static void Main()
        {
            //int[] numbers = [8, 3, 10, 1, 6, 14, 4, 7, 13];
            int[] numbers = [44,17,62,32,50,78,48,54,70,88];

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


            AVLT<int> avltTest = new();

            foreach (int number in numbers)
            {
                avltTest.Insert(number);
            }

            avltTest.Delete(32);
            //avltTest.RightRotation(avltTest.Search(15));

            Console.WriteLine("ajal");
        }
    }
}
