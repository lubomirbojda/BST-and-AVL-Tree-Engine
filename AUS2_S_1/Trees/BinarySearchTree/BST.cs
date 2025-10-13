using AUS2_S_1.Trees.BaseTree;

namespace AUS2_S_1.Trees.BinarySearchTree
{
    public class BST<T> : BaseBST<BSTNode<T>, T> where T : IComparable<T>
    {
        protected override BSTNode<T> CreateNode(T data)
        {
            return new BSTNode<T>(data);
        }
    }
}
