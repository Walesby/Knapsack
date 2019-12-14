using System;
using System.Collections.Generic;
using System.Text;

namespace Knapsack
{
    class MemoItem
    {
        public double Value { get; set; }
        public List<Items> List { get; set; }
        public MemoItem()
        {
            this.Value = 0;
            this.List = new List<Items>();
        }
        public MemoItem(double value, List<Items> list)
        {
            this.Value = value;
            this.List = list;
        }
        public override string ToString()
        {
            string result = "";
            foreach (var item in List)
            {
                result += $"[{item.Name}]\t";
            }
            return string.Format($"Value: {Value} \t\tContents: {result}\n");
        }
    }
}
