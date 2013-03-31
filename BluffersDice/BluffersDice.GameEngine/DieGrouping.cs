using System;
using System.Collections.Generic;
using System.Linq;


namespace BluffersDice.GameEngine
{
    [System.Diagnostics.DebuggerDisplay("Value: {DieValue}  Count: {Count}")]
    public class DieGrouping  : ICloneable
    {
        public DieGrouping(int value, int count)
        {
            DieValue = value;
            Count = count;
        }

        public int Count { get; set; }
        public int DieValue { get; set; }

        public object Clone()
        {
            return new DieGrouping(DieValue, Count);
        }
    }
}
