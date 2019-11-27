using System;

namespace NewPayDataTransformer.Model
{
    public class Column : IComparable<Column>
    {
        public string MaskType { get; set; }
        public int Position { get; set; }

        public int CompareTo(Column other)
        {
            return this.Position.CompareTo(other.Position);
        }

    }//end column
}//end namespace