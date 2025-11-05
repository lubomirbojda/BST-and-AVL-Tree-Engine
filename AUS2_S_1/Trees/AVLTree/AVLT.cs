using AUS2_S_1.Trees.BaseTree;

namespace AUS2_S_1.Trees.AVLTree
{
    public class AVLT<T> : BaseBST<AVLTNode<T>, T> where T : IComparable<T>
    {
        protected override AVLTNode<T> CreateNode(T data)
        {
            return new AVLTNode<T>(data);
        }

        public override bool Insert(T data)
        {
            var insertedNode = InsertNode(data);
            if (insertedNode != null)
            {
                //CheckAndRebalance(insertedNode.Parent, true);
                RebalanceAfterInsert(insertedNode.Parent);
                return true;
            }
            return false;
        }

        public override bool Delete(T data)
        {
            var helpNode = FindNode(data);
            if (helpNode != null)
            {
                var rebalancingNode = DeleteNode(helpNode);
                RebalanceAfterDelete(rebalancingNode);
                //CheckAndRebalance(rebalancingNode, false);
                return true;
            }
            
            return false;     
        }


        private void RightRotation(AVLTNode<T> node)
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

            node.RecalculateHeight();
            leftSon.RecalculateHeight();


        }

        private void LeftRotation(AVLTNode<T> node)
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

            node.RecalculateHeight();
            rightSon.RecalculateHeight();

        }

        private void RebalanceAfterInsert(AVLTNode<T>? node)
        {
            while (node != null)
            {
                node.RecalculateHeight();
                int nodeBF = node.GetBalanceFactor();

                if (nodeBF == 0) 
                    return;

                if (nodeBF > 1)
                {
                    if (node.LeftSon!.GetBalanceFactor() >= 0)
                    {
                        RightRotation(node);
                    }
                    else
                    {
                        LeftRotation(node.LeftSon);
                        RightRotation(node);
                    }
                    return;
                }

                if (nodeBF < -1)
                {
                    if (node.RightSon!.GetBalanceFactor() <= 0)
                    {
                        LeftRotation(node);
                    }
                    else
                    {
                        RightRotation(node.RightSon);
                        LeftRotation(node);
                    }
                    return;
                }

                node = node.Parent;
            }
        }

        private void RebalanceAfterDelete(AVLTNode<T>? node)
        {
            while (node != null)
            {
                int heightBeforeRebalance = node.Height;
                node.RecalculateHeight();
                var nodeBF = node.GetBalanceFactor();


                if (nodeBF > 1)
                {
                    if (node.LeftSon!.GetBalanceFactor() >= 0)
                    {
                        RightRotation(node);
                    }
                    else
                    {
                        LeftRotation(node.LeftSon);
                        RightRotation(node);
                    }
                }

                if (nodeBF < -1)
                {
                    if (node.RightSon!.GetBalanceFactor() <= 0)
                    {
                        LeftRotation(node);
                    }
                    else
                    {
                        RightRotation(node.RightSon);
                        LeftRotation(node);
                    }
                }

                if (Math.Abs(nodeBF) == 1 && heightBeforeRebalance == node.Height)
                    return;

                node = node.Parent;
            }
        }

        public bool IsAVLTBalanced()
        {
            if (Root == null) return true;

            foreach (var node in InOrder(Root))
            {
                node.RecalculateHeight();
                var bf = node.GetBalanceFactor();
                if (bf < -1 || bf > 1)
                    return false;
            }

            return true;
        }

    }
}
