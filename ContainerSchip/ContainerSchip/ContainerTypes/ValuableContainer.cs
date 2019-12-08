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
            if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.Stacks)).First(s => !s.DoesStackContainValuable()).TryAddContainer(this))
            {
                return true;
            }
            return ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.Stacks)).First(s => !s.DoesStackContainValuable()).TryAddContainer(this);
        }

        public bool TryPlaceOnImbalancedShip(Ship ship)
        {
            if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.GetLightestSideOfShip())).First(s => !s.DoesStackContainValuable())
                .TryAddContainer(this))
            {
                return true;
            }

            if (ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.GetLightestSideOfShip())).First(s => !s.DoesStackContainValuable())
                .TryAddContainer(this))
            {
                return true;
            }

            if (!ship.IsShipWidthEven())
            {
                if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.GetCentreStacks())).First(s => !s.DoesStackContainValuable())
                    .TryAddContainer(this))
                {
                    return true;
                }
                if (ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.GetCentreStacks())).First(s => !s.DoesStackContainValuable())
                    .TryAddContainer(this))
                {
                    return true;
                }
            }

            if (!ship.WillShipCapsizeIfContainerIsAdded(Weight))
            {
                if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.GetHeaviestSideOfShip())).First(s => !s.DoesStackContainValuable())
                    .TryAddContainer(this))
                {
                    return true;
                }
                if (ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.GetHeaviestSideOfShip())).First(s => !s.DoesStackContainValuable())
                    .TryAddContainer(this))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
