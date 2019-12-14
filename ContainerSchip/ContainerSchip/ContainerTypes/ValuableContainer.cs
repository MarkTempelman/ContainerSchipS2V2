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
        public ContainerType Type { get; } = ContainerType.Valuable;

        private static int _minWeight = 4000;
        private static int _maxWeight = 30000;


        public ValuableContainer(int weight)
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
            Stack stack = ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.Stacks))
                .FirstOrDefault(s => !s.DoesStackContainValuable());

            if (stack != null)
            {
                if(stack.TryAddContainer(this))
                    return true;
            }

            stack = ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.Stacks))
                .FirstOrDefault(s => !s.DoesStackContainValuable());

            if (stack != null)
            {
                return stack.TryAddContainer(this);
            }
            return false;
        }

        public bool TryPlaceOnImbalancedShip(Ship ship)
        {
            if (TryPlaceOnLightestSide(ship))
            {
                return true;
            }

            if (!ship.IsShipWidthEven())
            {
                if (TryPlaceInCentre(ship))
                {
                    return true;
                }
            }

            if (!ship.WillShipCapsizeIfContainerIsAdded(Weight))
            {
                return TryPlaceOnHeaviestSide(ship);
            }

            return false;
        }

        private bool TryPlaceOnLightestSide(Ship ship)
        {
            if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.GetLightestSideOfShip())).First(s => !s.DoesStackContainValuable())
                .TryAddContainer(this))
            {
                return true;
            }

            return ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.GetLightestSideOfShip())).First(s => !s.DoesStackContainValuable())
                .TryAddContainer(this);
        }

        private bool TryPlaceInCentre(Ship ship)
        {
            if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.GetCentreStacks())).First(s => !s.DoesStackContainValuable())
                .TryAddContainer(this))
            {
                return true;
            }

            return ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.GetCentreStacks())).First(s => !s.DoesStackContainValuable())
                .TryAddContainer(this);
        }

        private bool TryPlaceOnHeaviestSide(Ship ship)
        {
            if (ship.OrderStacksByWeightOnBottom(ship.GetRearStacks(ship.GetHeaviestSideOfShip())).First(s => !s.DoesStackContainValuable())
                .TryAddContainer(this))
            {
                return true;
            }

            return ship.OrderStacksByWeightOnBottom(ship.GetFrontStacks(ship.GetHeaviestSideOfShip())).First(s => !s.DoesStackContainValuable())
                .TryAddContainer(this);
        }
    }
}
