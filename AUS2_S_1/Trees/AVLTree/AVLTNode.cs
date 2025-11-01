using AUS2_S_1.Trees.BaseTree;

namespace AUS2_S_1.Trees.AVLTree
{
    public class AVLTNode<T> : BaseBSTNode<AVLTNode<T>, T> where T : IComparable<T>
    {
        public int Height { get; set; }

        public AVLTNode(T data) : base(data)
        {
            Height = 0;
        }

        public void RecalculateHeight()
        {
            int leftSonHeight = (LeftSon != null) ? LeftSon.Height : -1;
            int rightSonHeight = (RightSon != null) ? RightSon.Height : -1;

            Height = 1 + Math.Max(leftSonHeight, rightSonHeight);
        }

        public int GetBalanceFactor()
        {
            int leftSonHeight = (LeftSon != null) ? LeftSon.Height : -1;
            int rightSonHeight = (RightSon != null) ? RightSon.Height : -1;

            return leftSonHeight - rightSonHeight;
        }
    }
}
