using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchip.ContainerTypes
{
    public class ValuableContainer : IContainer
    {
        public int Weight { get; }
        public ContainerType Type { get; }


        public ValuableContainer(int weight, ContainerType type)
        {
            Weight = weight;
            Type = type;
        }

        public bool TryPlaceOnBalancedShip(Ship ship)
        {
            if (ship.OrderStacksByWeightOnBottom(ship.GetRearRow()).First().TryAddContainer(this))
            {
                return true;
            }
            return ship.OrderStacksByWeightOnBottom(ship.GetFrontRow()).First().TryAddContainer(this);
        }

        public bool TryPlaceOnImbalancedShip(Ship ship)
        {
            throw new NotImplementedException();
        }
    }
}
