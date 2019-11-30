using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchip
{
    public class Container
    {
        public int Weight { get; }
        public ContainerType Type { get; }

        public Container(int weight, ContainerType type)
        {
            Weight = weight;
            Type = type;
        }
    }
}
