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

        //not done
        public void RightRotation(Node<T> node)
        {
            var leftSon = node.LeftSon;
            var leftSonLeftSon = node.LeftSon!.LeftSon;
            var leftSonRightSon = node.LeftSon!.RightSon;



            //if node at which rotation is going to be done is root
            if (node.Parent == null && Root != null)
            {
                Root = leftSon;
                Root.RightSon = node;
                
                Root!.Parent = null;
                Root.LeftSon!.Parent = Root;
                Root.RightSon!.Parent = Root;

                return;
            }


            if (node == node.Parent.LeftSon)
            {

            }
        }

    }
}
