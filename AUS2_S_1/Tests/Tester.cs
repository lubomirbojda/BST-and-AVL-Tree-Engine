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

        public void TestAVL()
        {
            var rand = new Random();
            var avl = new AVLT<int>();
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
                    hsData.Add(value);
                }
                //delete
                else if (operation < 0.8)
                {
                    if (hsData.Count > 0)
                    {
                        var randInTree = rand.Next(0, hsData.Count);
                        value = hsData.ElementAt(randInTree);
                        avl.Delete(value);
                        hsData.Remove(value);
                    }
                }
                //find
                else
                {
                    if (hsData.Count > 0)
                    {
                        var randInTree = rand.Next(0, hsData.Count);
                        value = hsData.ElementAt(randInTree);
                        bool found = avl.Find(value) != default;
                        bool shouldExist = hsData.Contains(value);

                        if (found != shouldExist)
                        {
                            Console.WriteLine("Error at finding.");
                            return;
                        }
                    }
                    
                }

                if (i % 5000 == 0)
                {
                    if (!IsInOrderSorted(avl))
                    {
                        Console.WriteLine("InOrder not in order.");
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

        private bool IsInOrderSorted(AVLT<int> avltree)
        {
            var list = avltree.InOrder().ToList();
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i - 1].CompareTo(list[i]) > 0)
                    return false;
            }
            return true;
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
