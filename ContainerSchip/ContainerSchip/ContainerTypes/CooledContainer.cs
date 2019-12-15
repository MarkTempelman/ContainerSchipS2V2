using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchip.ContainerTypes
{
    public class CooledContainer : IContainer
    {
        public int Weight { get; }
        public ContainerType Type { get; } = ContainerType.Cooled;

        private static int _minWeight = 4000;
        private static int _maxWeight = 30000;

        public CooledContainer(int weight)
        {
            if (weight < _minWeight)
            {
                Weight = _minWeight;
            }
            else if (weight > _maxWeight)
            {
                Weight = _maxWeight;
            }
            else
            {
                Weight = weight;
            }
        }

        public bool TryPlaceOnBalancedShip(Ship ship)
        {
            return ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.Stacks)).First().TryAddContainer(this);
        }
    }
}
