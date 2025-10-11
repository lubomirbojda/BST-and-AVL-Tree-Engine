namespace AUS2_S_1.Trees.BinarySearchTree
{
    public class BST<T> where T : IComparable<T>
    {
        public Node<T>? Root { get; set; }

        public Node<T> Insert(T data)
        {
            if (Root == null)
            {
                Root = new Node<T>(data);
                return Root;
            }

            var helpNode = Root;

            while (true)
            {
                if (helpNode.Data.CompareTo(data) < 0)
                {
                    //when helpNode key is lower than insertNode key
                    if (helpNode.RightSon == null)
                    {
                        helpNode.RightSon = new Node<T>(data);
                        helpNode.RightSon.Parent = helpNode;
                        return helpNode.RightSon;
                    }
                    else
                    {
                        helpNode = helpNode.RightSon;
                    }
                }
                else if (helpNode.Data.CompareTo(data) > 0)
                {
                    //when insertNode key is lower than helpNode key
                    if (helpNode.LeftSon == null)
                    {
                        helpNode.LeftSon = new Node<T>(data);
                        helpNode.LeftSon.Parent = helpNode;
                        return helpNode.LeftSon;
                    }
                    else
                    {
                        helpNode = helpNode.LeftSon;
                    }
                }
                else
                {
                    //when key is already in tree
                    helpNode.Data = data;
                    return helpNode;
                }
            }
        }

        public Node<T>? Search(T data)
        {
            if (Root == null)
                return null;

            var helpNode = Root;

            while (true)
            {
                if (helpNode == null)
                    return null;

                if (helpNode.Data.CompareTo(data) < 0)
                {
                    //when helpNode key is lower than searching key
                    helpNode = helpNode.RightSon;
                }
                else if (helpNode.Data.CompareTo(data) > 0)
                {
                    //when searching key is lower than helpNode key
                    helpNode = helpNode.LeftSon;
                }
                else
                {
                    //when searching key is found
                    return helpNode;
                }
            }
        }

        public void Delete(T data)
        {
            var helpNode = Search(data);
            if (helpNode != null)
                DeleteNode(helpNode);
        }

        private void DeleteNode(Node<T> nodeToDelete)
        {
            if (nodeToDelete == null)
                return;

            //when deleting node is leaf
            if (nodeToDelete.IsLeaf())
            {
                if (nodeToDelete.Parent == null)
                {
                    Root = null;
                    return;
                }

                if (nodeToDelete == nodeToDelete.Parent.LeftSon)
                {
                    nodeToDelete.Parent.LeftSon = null;
                    return;
                }
                else
                {
                    nodeToDelete.Parent.RightSon = null;
                    return;
                }
            }


            //when deleting note has only 1 son
            if (nodeToDelete.LeftSon != null && nodeToDelete.RightSon == null)
            {
                if (nodeToDelete.Parent == null)
                {
                    Root = nodeToDelete.LeftSon;
                    return;
                }
                if (nodeToDelete == nodeToDelete.Parent.LeftSon)
                {
                    nodeToDelete.Parent.LeftSon = nodeToDelete.LeftSon;
                    nodeToDelete.LeftSon.Parent = nodeToDelete.Parent;
                    return;
                }
                else
                {
                    nodeToDelete.Parent.RightSon = nodeToDelete.LeftSon;
                    nodeToDelete.LeftSon.Parent = nodeToDelete.Parent;
                    return;
                }
            }
            else if (nodeToDelete.LeftSon == null && nodeToDelete.RightSon != null)
            {
                if (nodeToDelete.Parent == null)
                {
                    Root = nodeToDelete.RightSon;
                    return;
                }
                if (nodeToDelete == nodeToDelete.Parent.LeftSon)
                {
                    nodeToDelete.Parent.LeftSon = nodeToDelete.RightSon;
                    nodeToDelete.RightSon.Parent = nodeToDelete.Parent;
                    return;
                }
                else
                {
                    nodeToDelete.Parent.RightSon = nodeToDelete.RightSon;
                    nodeToDelete.RightSon.Parent = nodeToDelete.Parent;
                    return;
                }
            }
            //when deleting note has 2 sons --- not done :/
            else if (nodeToDelete.LeftSon != null && nodeToDelete.RightSon != null)
            {
                var minRight = FindMinimum(nodeToDelete.RightSon);
                nodeToDelete.Data = minRight.Data;


                if (minRight == minRight.Parent!.LeftSon)
                {
                    if (minRight.IsLeaf())
                    {
                        minRight.Parent.LeftSon = null;
                        return;
                    }
                    else
                    {
                        minRight.Parent.LeftSon = minRight.RightSon;
                        return;
                    }

                }
                else
                {
                    if (minRight.IsLeaf())
                    {
                        minRight.Parent.RightSon = null;
                        return;
                    }
                    else
                    {
                        minRight.Parent.RightSon = minRight.RightSon;
                        return;
                    }
                }
            }
        }

        public Node<T> FindMinimum(Node<T> startingNode)
        {
            var helpNode = startingNode;
            while (helpNode.LeftSon != null)
            {
                helpNode = helpNode.LeftSon;
            }
            return helpNode;
        }

    }
}
