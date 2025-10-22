using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using AUS2_S_1.Trees.AVLTree;
using AUS2_S_1.Trees.BinarySearchTree;

namespace AUS2_S_1.Tests
{
    public class Tester
    {
        private Stopwatch swBst = new();
        private Stopwatch swAvlt = new();
        private Stopwatch swSs = new();
        private BST<int> bstTest = new();
        private AVLT<int> avltTest = new();
        private SortedSet<int> sortedSetTest = new();

        public void TestOperationsTime()
        {
            // INSERT ------------------------------
            ResetTestsSetup();
            int numberOfElements = 10_000_000;
            Console.WriteLine("Inserting " + numberOfElements + " elements...");
            int[] elements = GetRandomValues(numberOfElements);

            swAvlt.Start();
            foreach (var value in elements)
            {
                avltTest.Insert(value);
            }
            swAvlt.Stop();

            swBst.Start();
            foreach (var value in elements)
            {

                bstTest.Insert(value);
            }
            swBst.Stop();

            swSs.Start();
            foreach (var value in elements)
            {
                sortedSetTest.Add(value);
            }
            swSs.Stop();


            PrintResult("Insert on BST", numberOfElements, swBst.Elapsed);
            PrintResult("Insert on AVLT", numberOfElements, swAvlt.Elapsed);
            PrintResult("Insert on SortedSet", numberOfElements, swSs.Elapsed);
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine();

            // DELETE ------------------------------
            ResetTestsSetup();
            var rand = new Random();
            int numberOfElementsToDelete = 2_000_000;
            Console.WriteLine("Deleting " + numberOfElementsToDelete + " elements...");
            rand.Shuffle(elements);
            var helpArray = elements.Take(numberOfElementsToDelete).ToArray();
            elements = elements.Skip(numberOfElementsToDelete).ToArray();

            swAvlt.Start();
            foreach (var value in helpArray)
            {
                avltTest.Delete(value);
            }
            swAvlt.Stop();

            swBst.Start();
            foreach (var value in helpArray)
            {
                bstTest.Delete(value);
            }
            swBst.Stop();

            swSs.Start();
            foreach (var value in elements)
            {
                sortedSetTest.Remove(value);
            }
            swSs.Stop();

            PrintResult("Delete on BST", numberOfElementsToDelete, swBst.Elapsed);
            PrintResult("Delete on AVLT", numberOfElementsToDelete, swAvlt.Elapsed);
            PrintResult("Delete on SortedSet", numberOfElementsToDelete, swSs.Elapsed);
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine();

            // FIND ------------------------------
            ResetTestsSetup();
            rand = new Random();
            int numberOfElementsToFind = 5_000_000;
            Console.WriteLine("Finding " + numberOfElementsToFind + " elements...");
            rand.Shuffle(elements);
            helpArray = elements.Take(numberOfElementsToFind).ToArray();

            swAvlt.Start();
            foreach (var value in helpArray)
            {
                avltTest.Find(value);
            }
            swAvlt.Stop();

            swBst.Start();
            foreach (var value in helpArray)
            {
                bstTest.Find(value);
            }
            swBst.Stop();

            swSs.Start();
            foreach (var value in helpArray)
            {
                sortedSetTest.Contains(value);
            }
            swSs.Stop();

            PrintResult("Find on BST", numberOfElementsToFind, swBst.Elapsed);
            PrintResult("Find on AVLT", numberOfElementsToFind, swAvlt.Elapsed);
            PrintResult("Find on SortedSet", numberOfElementsToFind, swSs.Elapsed);
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine();


            // INTERVAL FIND ------------------------------
            ResetTestsSetup();
            rand = new Random();
            int seed = rand.Next();
            int numberOfTests = 1_000_000;
            int minResultsPerInterval = 500;
            Console.WriteLine("Interval finding " + minResultsPerInterval + " elements in interval, " + numberOfTests + " times...");
            helpArray = elements.OrderBy(x => x).ToArray();

            rand = new Random(seed);
            swAvlt.Start();
            for (int i = 0; i < numberOfTests; i++)
            {
                int startIndex = rand.Next(0, helpArray.Length - minResultsPerInterval);
                int min = helpArray[startIndex];
                int max = helpArray[startIndex + minResultsPerInterval];
                var interval = avltTest.FindInterval(min, max).ToList();
            }
            swAvlt.Stop();

            rand = new Random(seed);
            swBst.Start();
            for (int i = 0; i < numberOfTests; i++)
            {
                int startIndex = rand.Next(0, helpArray.Length - minResultsPerInterval);
                int min = helpArray[startIndex];
                int max = helpArray[startIndex + minResultsPerInterval];
                var interval = bstTest.FindInterval(min, max).ToList();
            }
            swBst.Stop();

            rand = new Random(seed);
            swSs.Start();
            for (int i = 0; i < numberOfTests; i++)
            {
                int startIndex = rand.Next(0, helpArray.Length - minResultsPerInterval);
                int min = helpArray[startIndex];
                int max = helpArray[startIndex + minResultsPerInterval];
                var interval = sortedSetTest.GetViewBetween(min, max).ToList();
            }
            swSs.Stop();


            Console.WriteLine("\t Interval Find on BST:");
            Console.WriteLine("\t Elapsed time: " + swBst.Elapsed.TotalMilliseconds + " ms" + "  =  " + swBst.Elapsed.TotalSeconds + " s");
            Console.WriteLine("\t Interval Find on AVLT:");
            Console.WriteLine("\t Elapsed time: " + swAvlt.Elapsed.TotalMilliseconds + " ms" + "  =  " + swAvlt.Elapsed.TotalSeconds + " s");
            Console.WriteLine("\t Interval Find on SortedSet:");
            Console.WriteLine("\t Elapsed time: " + swSs.Elapsed.TotalMilliseconds + " ms" + "  =  " + swSs.Elapsed.TotalSeconds + " s");
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine();

            // FIND MINIMUM ------------------------------
            ResetTestsSetup();
            int numberOfReps = 2_000_000;
            Console.WriteLine("Finding minimum " + numberOfReps + " times...");

            swAvlt.Start();
            for (int i = 0; i < numberOfReps; i++)
            {
                var min = avltTest.FindMinimum();
            }
            swAvlt.Stop();

            swBst.Start();
            for (int i = 0; i < numberOfReps; i++)
            {
                var min = bstTest.FindMinimum();
            }
            swBst.Stop();

            swSs.Start();
            for (int i = 0; i < numberOfReps; i++)
            {
                var min = sortedSetTest.Min;
            }
            swSs.Stop();


            Console.WriteLine("\t Find minimum on BST:");
            Console.WriteLine("\t Elapsed time: " + swBst.Elapsed.TotalMilliseconds + " ms" + "  =  " + swBst.Elapsed.TotalSeconds + " s");
            Console.WriteLine("\t Find minimum on AVLT:");
            Console.WriteLine("\t Elapsed time: " + swAvlt.Elapsed.TotalMilliseconds + " ms" + "  =  " + swAvlt.Elapsed.TotalSeconds + " s");
            Console.WriteLine("\t Find minimum on SortedSet:");
            Console.WriteLine("\t Elapsed time: " + swSs.Elapsed.TotalMilliseconds + " ms" + "  =  " + swSs.Elapsed.TotalSeconds + " s");
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine();

            // FIND MAXIMUM ------------------------------
            ResetTestsSetup();
            numberOfReps = 2_000_000;
            Console.WriteLine("Finding maximum " + numberOfReps + " times...");

            swAvlt.Start();
            for (int i = 0; i < numberOfReps; i++)
            {
                var min = avltTest.FindMaximum();
            }
            swAvlt.Stop();

            swBst.Start();
            for (int i = 0; i < numberOfReps; i++)
            {
                var min = bstTest.FindMaximum();
            }
            swBst.Stop();

            swSs.Start();
            for (int i = 0; i < numberOfReps; i++)
            {
                var min = sortedSetTest.Max;
            }
            swSs.Stop();

            Console.WriteLine("\t Find maximum on BST:");
            Console.WriteLine("\t Elapsed time: " + swBst.Elapsed.TotalMilliseconds + " ms" + "  =  " + swBst.Elapsed.TotalSeconds + " s");
            Console.WriteLine("\t Find maximum on AVLT:");
            Console.WriteLine("\t Elapsed time: " + swAvlt.Elapsed.TotalMilliseconds + " ms" + "  =  " + swAvlt.Elapsed.TotalSeconds + " s");
            Console.WriteLine("\t Find maximum on SortedSet:");
            Console.WriteLine("\t Elapsed time: " + swSs.Elapsed.TotalMilliseconds + " ms" + "  =  " + swSs.Elapsed.TotalSeconds + " s");
            Console.WriteLine();
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine();
        }

        private void ResetTestsSetup()
        {
            swAvlt.Reset();
            swBst.Reset();
            swSs.Reset();
        }

        public void TestAVLandBST()
        {
            var rand = new Random();
            var avl = new AVLT<int>();
            var bst = new BST<int>();
            var hsData = new HashSet<int>();

            int operations = 1_000_000;

            for (int i = 0; i < operations; i++)
            {
                double operation = rand.NextDouble();
                int value = rand.Next(1, 10_000);

                //insert
                if (operation < 0.4)
                {
                    avl.Insert(value);
                    bst.Insert(value);
                    hsData.Add(value);
                }
                //delete
                else if (operation < 0.7)
                {
                    if (hsData.Count > 0)
                    {
                        var randInTree = rand.Next(0, hsData.Count);
                        value = hsData.ElementAt(randInTree);
                        if (!avl.Delete(value))
                        {
                            Console.WriteLine("Error at deleting - AVL.");
                            return;
                        }
                        if (!bst.Delete(value))
                        {
                            Console.WriteLine("Error at deleting - BST.");
                            return;
                        }
                        hsData.Remove(value);
                    }
                }
                //find
                else if (operation < 0.8)
                {
                    if (hsData.Count > 0)
                    {
                        var randInTree = rand.Next(0, hsData.Count);
                        value = hsData.ElementAt(randInTree);
                        bool foundAVL = avl.Find(value) != default;
                        bool foundBST = bst.Find(value) != default;
                        bool shouldExist = hsData.Contains(value);

                        if (foundAVL != shouldExist)
                        {
                            Console.WriteLine("Error at finding - AVL.");
                            return;
                        }
                        if (foundBST != shouldExist)
                        {
                            Console.WriteLine("Error at finding - BST.");
                            return;
                        }
                    }

                }
                //interval find
                else
                {
                    var interval = hsData.OrderBy(x => x).ToList();
                    var minIndex = rand.Next(0, interval.Count);
                    var maxIndex = rand.Next(minIndex, interval.Count);
                    var min = interval[minIndex];
                    var max = interval[maxIndex];
                    var intervalCompare = interval.GetRange(minIndex, maxIndex - minIndex + 1);
                    var bstIntFind = bst.FindInterval(min, max).ToArray();
                    var avlIntFind = avl.FindInterval(min, max).ToArray();

                    if (!bstIntFind.SequenceEqual(intervalCompare))
                    {
                        Console.WriteLine("Error at interval finding - BST.");
                        return;
                    }
                    if (!avlIntFind.SequenceEqual(intervalCompare))
                    {
                        Console.WriteLine("Error at interval finding - AVL.");
                        return;
                    }
                }

                if (i % 10 == 0)
                {
                    if (!avl.IsInOrderSorted())
                    {
                        Console.WriteLine("InOrder not in order - AVL");
                        return;
                    }

                    if (!bst.IsInOrderSorted())
                    {
                        Console.WriteLine("InOrder not in order - BST.");
                        return;
                    }

                    if (!avl.IsAVLTBalanced())
                    {
                        Console.WriteLine("AVLT is not balanced.");
                        return;
                    }
                }
            }

            Console.WriteLine("Test successful.");
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
