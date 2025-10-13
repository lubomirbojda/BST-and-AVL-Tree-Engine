namespace AUS2_S_1.Trees.BaseTree
{
    public abstract class BaseBSTNode<TNode, TData> where TData : IComparable<TData>
    {
        public TData Data { get; set; }
        public TNode? LeftSon { get; set; }
        public TNode? RightSon { get; set; }
        public TNode? Parent { get; set; }

        protected BaseBSTNode(TData data)
        {
            Data = data;
        }

        public bool IsLeaf()
        {
            if (LeftSon == null && RightSon == null)
                return true;
            return false;
        }
    }
}
