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
        private List<Stack> Stacks { get; } = new List<Stack>();
        private readonly int _maxWeightOfStack = 150000;
        private List<IContainer> _previousContainers = new List<IContainer>();

        public Ship(int length, int width)
        {
            Length = length;
            Width = width;
            CreateStacks();
        }

        public List<IContainer> PlaceContainers(List<IContainer> containers)
        {
            containers = OrderContainers(containers);
            foreach (var container in containers)
            {
                if (IsShipBalanced())
                {
                    if (container.TryPlaceOnBalancedShip(this))
                    {
                        containers.Remove(container);
                    }
                }
                else
                {
                    if (container.TryPlaceOnImbalancedShip(this))
                    {
                        containers.Remove(container);
                    }
                }
            }

            if (HasContainerListChanged(containers))
            {
                _previousContainers = containers;
                return PlaceContainers(containers);
            }

            return containers;
        }

        public bool HasContainerListChanged(List<IContainer> containers)
        {
            return _previousContainers != containers;
        }

        public List<Stack> GetLightestSideOfShip()
        {
            if
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

        private bool IsShipWidthEven()
        {
            return Width % 2 == 0;
        }

        public List<Stack> GetFrontRow()
        {
            return Stacks.Where(s => s.LengthCoordinates == 1).ToList();
        }

        public List<Stack> GetRearRow()
        {
            return Stacks.Where(s => s.LengthCoordinates == Length).ToList();
        }

        public List<Stack> GetCoreRows()
        {
            return Stacks.Where(s => s.LengthCoordinates > 1 && s.LengthCoordinates < Length).ToList();
        }

        private bool IsShipBalanced()
        {
            return GetLeftWeight(GetLeftMaxRow()) == GetRightWeight(GetRightMinRow());
        }

        public List<Stack> OrderStacksByWeightOnBottom(List<Stack> stacks)
        {
            return stacks.OrderBy(c => c.GetWeightOnBottomContainer()).ToList();
        }

        public int GetCentreRow()
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

        private List<IContainer> OrderContainers(List<IContainer> containers)
        {
            return containers.OrderBy(c => c.Type).ThenByDescending(c => c.Weight).ToList();
        }

        private int WhatIs50PercentWeight()
        {
            return _maxWeightOfStack * Width * Length;
        }
    }
}
