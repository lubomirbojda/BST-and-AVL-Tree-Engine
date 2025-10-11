using System.Security.Cryptography;
using AUS2_S_1.Trees.BinarySearchTree;

namespace AUS2_S_1.Trees.AVLTree
{
    public class AVLT<T> : BST<T> where T : IComparable<T>
    {

        public new Node<T> Insert(T data)
        {
            var insertedNode = base.Insert(data);
            insertedNode.Height = 0;
            insertedNode.BF = 0;

            return insertedNode;
        }

        public void RightRotation(Node<T> node)
        {
            var leftSon = node.LeftSon;
            var leftSonRightSon = node.LeftSon!.RightSon;

            if (node.Parent == null)
            {
                Root = leftSon;
            }
            else if (node == node.Parent.LeftSon)
            {
                node.Parent.LeftSon = leftSon;
            }
            else
            {
                node.Parent.RightSon = leftSon;
            }
            leftSon!.Parent = node.Parent;

            node.LeftSon = leftSonRightSon;
            if (leftSonRightSon != null)
            {
                leftSonRightSon.Parent = node;
            }

            leftSon.RightSon = node;
            node.Parent = leftSon;
        }

        public void LeftRotation(Node<T> node)
        {
            var rightSon = node.RightSon;
            var rightSonLeftSon = node.RightSon!.LeftSon;

            if (node.Parent == null)
            {
                Root = rightSon;
            }
            else if (node == node.Parent.RightSon)
            {
                node.Parent.RightSon = rightSon;
            }
            else
            {
                node.Parent.LeftSon = rightSon;
            }

            rightSon!.Parent = node.Parent;

            node.RightSon = rightSonLeftSon;
            if (rightSonLeftSon != null)
            {   
                rightSonLeftSon.Parent = node;
            }

            rightSon.LeftSon = node;
            node.Parent = rightSon;
        }

    }
}
