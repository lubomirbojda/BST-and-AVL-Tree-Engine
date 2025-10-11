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
            else
            {
                node.Parent.LeftSon = leftSon;
            }
            leftSon!.Parent = node.Parent;


            if (leftSonRightSon != null)
            {
                node.LeftSon = leftSonRightSon;
                leftSonRightSon.Parent = node;
            }
            else
            {
                node.LeftSon = null;
            }
            leftSon.RightSon = node;
            node.Parent = leftSon;
        }

    }
}
