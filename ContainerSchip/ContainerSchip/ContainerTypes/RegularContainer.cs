using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchip.ContainerTypes
{
    public class RegularContainer : IContainer
    {
        public int Weight { get; }
        public ContainerType Type { get; }

        public RegularContainer(int weight, ContainerType type)
        {
            Weight = weight;
            Type = type;
        }

        public bool TryPlaceOnBalancedShip(Ship ship)
        {
            if (ship.OrderStacksByWeightOnBottom(ship.GetCoreStacks(ship.Stacks)).First().TryAddContainer(this))
            {
                return true;
            }
            if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.Stacks)).First().TryAddContainer(this))
            {
                return true;
            }
            return ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.Stacks)).First().TryAddContainer(this);
        }

        public bool TryPlaceOnImbalancedShip(Ship ship)
        {
            throw new NotImplementedException();
        }
    }
}
