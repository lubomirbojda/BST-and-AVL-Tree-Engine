using AUS2_S_1.Trees.BaseTree;

namespace AUS2_S_1.Trees.BinarySearchTree
{
    public class BSTNode<T> : BaseBSTNode<BSTNode<T>, T> where T : IComparable<T>
    {
        public BSTNode(T data) : base(data)
        {
        }
    }
}
