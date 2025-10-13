namespace AUS2_S_1.Trees.BaseTree
{
    public abstract class BaseBST<TNode, TData> where TData : IComparable<TData> where TNode : BaseBSTNode<TNode, TData>
    {
        protected TNode? Root { get; set; }

        protected abstract TNode CreateNode(TData data);

        public virtual TNode Insert(TData data)
        {
            if (Root == null)
            {
                Root = CreateNode(data);
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
                        helpNode.RightSon = CreateNode(data);
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
                        helpNode.LeftSon = CreateNode(data);
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

        public virtual TNode? Search(TData data)
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

        public virtual TNode? Delete(TData data)
        {
            var helpNode = Search(data);
            return DeleteNode(helpNode);

        }

        //return parent node of deleting node if deleting leaf or node with one son
        //return parent node of minimum node in right subtree (replacing node)
        private TNode? DeleteNode(TNode? nodeToDelete)
        {
            if (nodeToDelete == null)
                return null;

            var returningNode = nodeToDelete.Parent;
            //when deleting node is leaf
            if (nodeToDelete.IsLeaf())
            {
                if (nodeToDelete.Parent == null)
                {
                    Root = null;
                    return returningNode;
                }

                if (nodeToDelete == nodeToDelete.Parent.LeftSon)
                {
                    nodeToDelete.Parent.LeftSon = null;
                    return returningNode;
                }
                else
                {
                    nodeToDelete.Parent.RightSon = null;
                    return returningNode;
                }
            }


            //when deleting note has only 1 son
            if (nodeToDelete.LeftSon != null && nodeToDelete.RightSon == null)
            {
                if (nodeToDelete.Parent == null)
                {
                    Root = nodeToDelete.LeftSon;
                    return returningNode;
                }

                if (nodeToDelete == nodeToDelete.Parent.LeftSon)
                {
                    nodeToDelete.Parent.LeftSon = nodeToDelete.LeftSon;
                    nodeToDelete.LeftSon.Parent = nodeToDelete.Parent;
                    return returningNode;
                }
                else
                {
                    nodeToDelete.Parent.RightSon = nodeToDelete.LeftSon;
                    nodeToDelete.LeftSon.Parent = nodeToDelete.Parent;
                    return returningNode;
                }
            }
            else if (nodeToDelete.LeftSon == null && nodeToDelete.RightSon != null)
            {
                if (nodeToDelete.Parent == null)
                {
                    Root = nodeToDelete.RightSon;
                    return returningNode;
                }

                if (nodeToDelete == nodeToDelete.Parent.LeftSon)
                {
                    nodeToDelete.Parent.LeftSon = nodeToDelete.RightSon;
                    nodeToDelete.RightSon.Parent = nodeToDelete.Parent;
                    return returningNode;
                }
                else
                {
                    nodeToDelete.Parent.RightSon = nodeToDelete.RightSon;
                    nodeToDelete.RightSon.Parent = nodeToDelete.Parent;
                    return returningNode;
                }
            }

            //when deleting note has 2 sons
            else if (nodeToDelete.LeftSon != null && nodeToDelete.RightSon != null)
            {
                var minRight = FindMinimum(nodeToDelete.RightSon);
                returningNode = minRight.Parent;
                nodeToDelete.Data = minRight.Data;


                if (minRight == minRight.Parent!.LeftSon)
                {
                    if (minRight.IsLeaf())
                    {
                        minRight.Parent.LeftSon = null;
                        return returningNode;
                    }
                    else
                    {
                        minRight.Parent.LeftSon = minRight.RightSon;
                        return returningNode;
                    }

                }
                else
                {
                    if (minRight.IsLeaf())
                    {
                        minRight.Parent.RightSon = null;
                        return returningNode;
                    }
                    else
                    {
                        minRight.Parent.RightSon = minRight.RightSon;
                        return returningNode;
                    }
                }
            }
            return null;
        }

        private TNode FindMinimum(TNode startingNode)
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
