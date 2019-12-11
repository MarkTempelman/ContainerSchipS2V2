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

        public CooledContainer(int weight)
        {
            Weight = weight;
        }

        public bool TryPlaceOnBalancedShip(Ship ship)
        {
            return ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.Stacks)).First().TryAddContainer(this);
        }

        public bool TryPlaceOnImbalancedShip(Ship ship)
        {
            if (ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.GetLightestSideOfShip())).First()
                .TryAddContainer(this))
            {
                return true;
            }

            if (!ship.IsShipWidthEven())
            {
                if (ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.GetCentreStacks())).First()
                    .TryAddContainer(this))
                {
                    return true;
                }
            }

            if (!ship.WillShipCapsizeIfContainerIsAdded(Weight))
            {
                if (ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.GetHeaviestSideOfShip())).First()
                    .TryAddContainer(this))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
