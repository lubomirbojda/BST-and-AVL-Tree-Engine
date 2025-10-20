using System.Diagnostics;
using AUS2_S_1.Trees.AVLTree;
using AUS2_S_1.Trees.BinarySearchTree;

namespace AUS2_S_1.Tests
{
    public class Tester
    {
        private Stopwatch swBst = new();
        private Stopwatch swAvlt = new();
        private BST<int> bstTest = new();
        private AVLT<int> avltTest = new();

        public void TestInsert()
        {
            // INSERT ------------------------------
            ResetTestsSetup();
            int numberOfElements = 10_000_000;
            Console.WriteLine("Inserting " + numberOfElements + " elements...");
            int[] elements = GetRandomValues(numberOfElements);
            int insertedCountAVL = 0;
            int insertedCountBST = 0;

            swAvlt.Start();
            foreach (var value in elements)
            {
                insertedCountAVL += avltTest.Insert(value) ? 1 : 0;
            }
            swAvlt.Stop();

            swBst.Start();
            foreach (var value in elements)
            {
               
                insertedCountBST += bstTest.Insert(value) ? 1 : 0;
            }
            swBst.Stop();

            PrintResult("Insert on BST", numberOfElements, swBst.Elapsed);
            PrintResult("Insert on AVLT", numberOfElements, swAvlt.Elapsed);
            Console.WriteLine("Inserted elements count on BST: " + insertedCountBST);
            Console.WriteLine("Inserted elements count on AVLT: " + insertedCountAVL);
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine();

            // DELETE ------------------------------
            ResetTestsSetup();
            var rand = new Random();
            int numberOfElementsToDelete = 2_000_000;
            Console.WriteLine("Deleting " + numberOfElementsToDelete + " elements...");
            int deletedCountAVL = 0;
            int deletedCountBST = 0;
            //int[] elementsToDelete = new int[numberOfElementsToDelete];
            rand.Shuffle(elements);
            var elementsToDelete = elements.Take(numberOfElementsToDelete).ToArray();

            swAvlt.Start();
            foreach (var value in elementsToDelete)
            {
                deletedCountAVL += avltTest.Delete(value) ? 1 : 0;
            }
            swAvlt.Stop();

            swBst.Start();
            foreach (var value in elementsToDelete)
            {
                deletedCountBST += bstTest.Delete(value) ? 1 : 0;
            }
            swBst.Stop();

            PrintResult("Delete on BST", numberOfElementsToDelete, swBst.Elapsed);
            PrintResult("Delete on AVLT", numberOfElementsToDelete, swAvlt.Elapsed);
            Console.WriteLine("Deleted elements count on BST: " + deletedCountBST);
            Console.WriteLine("Deleted elements count on AVLT: " + deletedCountAVL);
        }

        private void ResetTestsSetup()
        {
            swAvlt.Reset();
            swBst.Reset();
        }

        private int[] GetRandomValues(int numberOfValues)
        {
            var rand = new Random();
            var hsNum = new HashSet<int>();
            var arrNum = new int[numberOfValues];
            int i = 0;
            while (i < numberOfValues)
            {
                var r = rand.Next();
                if (hsNum.Add(r))
                {
                    arrNum[i] = r;
                    i++;
                }             
            }

            return arrNum;
        }

        private void PrintResult(string operationName, int numberOfElements, TimeSpan elapsedTime)
        {
            Console.WriteLine("Operation: " + operationName + ":");
            Console.WriteLine("\t" + "Number of elements: " + numberOfElements);
            Console.WriteLine("\t" + "Elapsed time: " + elapsedTime.TotalMilliseconds + " ms" + "  =  " + elapsedTime.TotalSeconds + " s");
            Console.WriteLine();
        }
    }
}
