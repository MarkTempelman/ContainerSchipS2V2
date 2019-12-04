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
            if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.Stacks)).First().TryAddContainer(this))
            {
                return true;
            }
            return ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.Stacks)).First().TryAddContainer(this);
        }

        public bool TryPlaceOnImbalancedShip(Ship ship)
        {
            if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.GetLightestSideOfShip())).First()
                .TryAddContainer(this))
            {
                return true;
            }

            if (ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.GetLightestSideOfShip())).First()
                .TryAddContainer(this))
            {
                return true;
            }

            if (!ship.IsShipWidthEven())
            {
                if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.GetCentreStacks())).First()
                    .TryAddContainer(this))
                {
                    return true;
                }
                if (ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.GetCentreStacks())).First()
                    .TryAddContainer(this))
                {
                    return true;
                }
            }

            if (!ship.WillShipCapsizeIfContainerIsAdded(Weight))
            {
                if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.GetHeaviestSideOfShip())).First()
                    .TryAddContainer(this))
                {
                    return true;
                }
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
