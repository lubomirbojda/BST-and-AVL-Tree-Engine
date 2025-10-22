namespace AUS2_S_1.Trees.BaseTree
{
    public abstract class BaseBST<TNode, TData> where TData : IComparable<TData> where TNode : BaseBSTNode<TNode, TData>
    {
        protected TNode? Root { get; set; }

        protected abstract TNode CreateNode(TData data);

        public virtual bool Insert(TData data)
        {
            if (InsertNode(data) == null)
                return false;

            return true;
        }

        protected virtual TNode? InsertNode(TData data)
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
                    //helpNode.Data = data;
                    return null;
                }
            }
        }

        public virtual TData? Find(TData data)
        {
            var foundData = FindNode(data);
            if (foundData == null)
                return default;

            return foundData.Data;
        }

        protected virtual TNode? FindNode(TData data)
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

        public virtual bool Delete(TData data)
        {
            var helpNode = FindNode(data);
            if (helpNode != null)
            {
                DeleteNode(helpNode);
                return true;
            }

            return false;
        }


        //return parent node of deleting node if deleting leaf or node with one son
        //return parent node of minimum node in right subtree (replacing node)
        protected virtual TNode? DeleteNode(TNode nodeToDelete)
        {

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
                    Root.Parent = null;
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
                    Root.Parent = null;
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
                        minRight.RightSon!.Parent = minRight.Parent;
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
                        minRight.RightSon!.Parent = minRight.Parent;
                        return returningNode;
                    }
                }
            }
            return returningNode;
        }

        protected IEnumerable<TNode> InOrder(TNode node)
        {
            if (node == null)
                yield break;

            var helpNode = node;
            var lastNode = node.Parent;
            TNode? nextNode;

            while (helpNode != null)
            {
                if (lastNode == helpNode.Parent)
                {
                    if (helpNode.LeftSon != null)
                    {
                        nextNode = helpNode.LeftSon;
                    }
                    else
                    {
                        yield return helpNode;
                        if (helpNode.RightSon != null)
                        {
                            nextNode = helpNode.RightSon;
                        }
                        else
                        {
                            nextNode = helpNode.Parent;
                        }
                    }
                }
                else
                {
                    if (lastNode == helpNode.LeftSon)
                    {
                        yield return helpNode;
                        if (helpNode.RightSon != null)
                        {
                            nextNode = helpNode.RightSon;
                        }
                        else
                        {
                            nextNode = helpNode.Parent;
                        }
                    }
                    else
                    {
                        nextNode = helpNode.Parent;
                    }
                }



                lastNode = helpNode;
                helpNode = nextNode;
            }
        }

        public IEnumerable<TData> InOrder()
        {
            if (Root == null)
                yield break;

            foreach (var node in InOrder(Root))
            {
                yield return node.Data;
            }
        }


        public IEnumerable<TData> FindInterval(TData min, TData max)
        {
            if (Root == null)
                yield break;

            var helpNode = Root;
            var lastNode = Root.Parent;
            TNode? nextNode;

            while (helpNode != null)
            {
                if (lastNode == helpNode.Parent)
                {
                    if (helpNode.LeftSon != null && helpNode.Data.CompareTo(min) > 0)
                    {
                        nextNode = helpNode.LeftSon;
                    }
                    else
                    {
                        if (helpNode.Data.CompareTo(min) >= 0 && helpNode.Data.CompareTo(max) <= 0)
                            yield return helpNode.Data;

                        if (helpNode.RightSon != null && helpNode.Data.CompareTo(max) < 0)
                        {
                            nextNode = helpNode.RightSon;
                        }
                        else
                        {
                            nextNode = helpNode.Parent;
                        }
                    }
                }
                else
                {
                    if (lastNode == helpNode.LeftSon)
                    {
                        if (helpNode.Data.CompareTo(min) >= 0 && helpNode.Data.CompareTo(max) <= 0)
                            yield return helpNode.Data;

                        if (helpNode.RightSon != null && helpNode.Data.CompareTo(max) < 0)
                        {
                            nextNode = helpNode.RightSon;
                        }
                        else
                        {
                            nextNode = helpNode.Parent;
                        }
                    }
                    else
                    {
                        nextNode = helpNode.Parent;
                    }
                }

                lastNode = helpNode;
                helpNode = nextNode;
            }
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

        public TData? FindMinimum()
        {
            if (Root == null)
                return default;

            return FindMinimum(Root).Data;
        }

        private TNode FindMaximum(TNode startingNode)
        {
            var helpNode = startingNode;
            while (helpNode.RightSon != null)
            {
                helpNode = helpNode.RightSon;
            }
            return helpNode;
        }

        public TData? FindMaximum()
        {
            if (Root == null)
                return default;

            return FindMaximum(Root).Data;
        }
    }
}
