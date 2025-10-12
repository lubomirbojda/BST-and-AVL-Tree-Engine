using AUS2_S_1.Trees.BinarySearchTree;

namespace AUS2_S_1.Trees.AVLTree
{
    public class AVLT<T> : BST<T> where T : IComparable<T>
    {

        public override Node<T> Insert(T data)
        {
            var insertedNode = base.Insert(data);
            var helpNode = insertedNode;

            CheckAndRebalance(helpNode);

            return insertedNode;
        }

        public override Node<T>? Delete(T data)
        {
            var helpNode = base.Delete(data);

            CheckAndRebalance(helpNode);

            return helpNode;
        }

        private void RightRotation(Node<T> node)
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

            node.RecalculateHeightAndBF();
            leftSon.RecalculateHeightAndBF();
        }

        private void LeftRotation(Node<T> node)
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

            node.RecalculateHeightAndBF();
            rightSon.RecalculateHeightAndBF();
        }

        private void CheckAndRebalance(Node<T>? node)
        {
            while (node != null)
            {
                node.RecalculateHeightAndBF();

                if (node.BF > 1)
                {
                    if (node.LeftSon!.BF >= 0)
                    {
                        RightRotation(node);
                    }
                    else
                    {
                        LeftRotation(node.LeftSon);
                        RightRotation(node);
                    }
                }

                if (node.BF < -1)
                {
                    if (node.RightSon!.BF <= 0)
                    {
                        LeftRotation(node);
                    }
                    else
                    {
                        RightRotation(node.RightSon);
                        LeftRotation(node);
                    }
                }

                node = node.Parent;
            }
        }

    }
}
