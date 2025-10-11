using AUS2_S_1.Trees.AVLTree;

namespace AUS2_S_1.Trees
{
    public class Node<T> where T : IComparable<T>
    {
        public T Data { get; set; }
        public Node<T>? LeftSon { get; set; }
        public Node<T>? RightSon { get; set; }
        public Node<T>? Parent { get; set; }
        public int Height { get; set; }
        public int BF { get; set; }

        public Node(T data)
        {
            Data = data;
        }

        public bool IsLeaf()
        {
            if (LeftSon == null && RightSon == null)
                return true;
            return false;
        }

        public void RecalculateHeightAndBF()
        {
            int leftSonHeight = LeftSon?.Height ?? -1;
            int rightSonHeight = RightSon?.Height ?? -1;

            Height = 1 + Math.Max(leftSonHeight, rightSonHeight);
            BF = leftSonHeight - rightSonHeight;
        }

        public void RecalculateHeightAndBFSubtree(Node<T> node)
        {

        }

        public void CheckAndRebalance(Node<T> node)
        {

        }
    }
}
