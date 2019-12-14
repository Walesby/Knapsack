using System;
using System.Collections.Generic;
using System.Text;

namespace Knapsack
{
    class Items
    {
        public int Weight { get; set; }
        public double Value { get; set; }
        public string Name { get; set; }
        public Items(int weight, double value, string name)
        {
            this.Weight = weight;
            this.Value = value;
            this.Name = name;
        }
    }
}
