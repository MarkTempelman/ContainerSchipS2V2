using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchip
{
    public class Ship
    {
        private int Length { get; }
        private int Width { get; }
        public List<Stack> Stacks { get; } = new List<Stack>();
        private readonly int _maxWeightOfStack = 150000;
        private List<IContainer> _previousContainers = new List<IContainer>();

        public Ship(int width, int length)
        {
            Width = width;
            Length = length;
            CreateStacks();
        }

        public List<IContainer> PlaceContainers(List<IContainer> currentContainers)
        {
            currentContainers = SortContainers(currentContainers);
            List<IContainer> newContainers = new List<IContainer>();
            foreach (var container in currentContainers)
            {
                if (IsShipBalanced())
                {
                    if (!container.TryPlaceOnBalancedShip(this))
                    {
                        newContainers.Add(container);
                    }
                }
                else
                {
                    if (!container.TryPlaceOnImbalancedShip(this))
                    {
                        newContainers.Add(container);
                    }
                }
            }

            if (HasContainerListChanged(newContainers) && newContainers.Count > 0)
            {
                _previousContainers = newContainers;
                return PlaceContainers(newContainers);
            }

            return newContainers;
        }

        public bool HasContainerListChanged(List<IContainer> containers)
        {
            return _previousContainers.Count != containers.Count;
        }

        public List<Stack> GetLightestSideOfShip()
        {
            if (GetLeftWeight(GetLeftMaxRow()) < GetRightWeight(GetRightMinRow()))
            {
                return GetLeftStacks();
            }
            return GetRightStacks();
        }

        public List<Stack> GetHeaviestSideOfShip()
        {
            if (GetLeftWeight(GetLeftMaxRow()) > GetRightWeight(GetRightMinRow()))
            {
                return GetLeftStacks();
            }
            return GetRightStacks();
        }

        private List<Stack> GetLeftStacks()
        {
            return Stacks.Where(s => s.WidthCoordinates <= GetLeftMaxRow()).ToList();
        }

        private List<Stack> GetRightStacks()
        {
            return Stacks.Where(s => s.WidthCoordinates >= GetRightMinRow()).ToList();
        }

        public List<Stack> GetCentreStacks()
        {
            return Stacks.Where(s => s.WidthCoordinates == GetCentreRow()).ToList();
        }

        private void CreateStacks()
        {
            for (int w = 1; w <= Width; w++)
            {
                for (int l = 1; l <= Length; l++)
                {
                    Stacks.Add(new Stack(w, l));
                }
            }
        }

        public bool IsShipWidthEven()
        {
            return Width % 2 == 0;
        }


        public List<Stack> GetFrontStacks(List<Stack> stacks)
        {
            return stacks.Where(s => s.LengthCoordinates == 1).ToList();
        }

        public List<Stack> GetRearStacks(List<Stack> stacks)
        {
            return stacks.Where(s => s.LengthCoordinates == Length).ToList();
        }

        public List<Stack> GetCoreStacks(List<Stack> stacks)
        {
            if (Length < 2)
            {
                return stacks;
            }

            if (Length < 3)
            {
                return stacks.Where(s => s.LengthCoordinates > 1).ToList();
            }
            return stacks.Where(s => s.LengthCoordinates > 1 && s.LengthCoordinates < Length).ToList();
        }

        private bool IsShipBalanced()
        {
            return GetLeftWeight(GetLeftMaxRow()) == GetRightWeight(GetRightMinRow());
        }

        public List<Stack> OrderStacksByWeightOnBottom(List<Stack> stacks)
        {
            return stacks.OrderBy(c => c.GetWeightOnBottomContainer()).ToList();
        }

        private int GetCentreRow()
        {
            double centre = (double) Width / 2;
            return Convert.ToInt32(Math.Ceiling(centre));
        }

        private int GetLeftMaxRow()
        {
            if (IsShipWidthEven())
            {
                return Width / 2;
            }

            return GetCentreRow() - 1;
        }

        private int GetRightMinRow()
        {
            if (IsShipWidthEven())
            {
                return (Width / 2 + 1);
            }

            return GetCentreRow() + 1;
        }

        private int GetLeftWeight(int leftMax)
        {
            return Stacks.Where(s => s.WidthCoordinates <= leftMax).Sum(s => s.GetTotalWeight());
        }

        private int GetRightWeight(int rightMin)
        {
            return Stacks.Where(s => s.WidthCoordinates >= rightMin).Sum(s => s.GetTotalWeight());
        }

        private List<IContainer> SortContainers(List<IContainer> containers)
        {
            return containers.OrderBy(c => c.Type).ThenByDescending(c => c.Weight).ToList();
        }

        private int WhatIs50PercentWeight()
        {
            return _maxWeightOfStack * Width * Length;
        }

        private int GetCurrentShipWeight()
        {
            return Stacks.Sum(stack => stack.GetTotalWeight());
        }

        public bool WillShipCapsizeIfContainerIsAdded(int weightToAdd)
        {
            if (GetLeftWeight(GetLeftMaxRow()) > GetRightWeight(GetRightMinRow()))
            {
                return (GetLeftWeight(GetLeftMaxRow()) + weightToAdd) / (GetCurrentShipWeight() + weightToAdd) * 100 > 60;
            }
            return (GetRightWeight(GetRightMinRow()) + weightToAdd) / (GetCurrentShipWeight() + weightToAdd) * 100 > 60;
        }
    }
}
