namespace Tree.Lib
{
    public class Meta
    {
        public Meta(int idx, int childCount, int metaCount, int[] metaInfo)
        {
            this.Index = idx;
            this.ChildCount = childCount;
            this.MetaCount = metaCount;
            this.MetaInfo = metaInfo;
        }
        public int Index { get; private set; }
        private int ChildCount { get; set; }
        private int MetaCount { get; set; }
        public int InfoLength
        {
            get
            {
                return MetaCount + 2;
            }
        }
        private int[] MetaInfo { get; set; }
    }
}
