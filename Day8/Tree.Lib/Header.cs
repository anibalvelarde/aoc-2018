namespace Tree.Lib
{
    public class Header
    {
        public int ChildrenCount { get; private set; }
        public int MetaDataItemCount { get; private set; }

        public Header(int childCount, int metaDataEntryCount)
        {
            this.ChildrenCount = childCount;
            this.MetaDataItemCount = metaDataEntryCount;
        }
    }
}
