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
        public int Length { get; }
        public int Width { get; }
        private List<Stack> Stacks { get; } = new List<Stack>();
        private readonly int _maxWeightOfStack = 150000;
        private List<Container> _previousContainers;

        public Ship(int length, int width)
        {
            Length = length;
            Width = width;
            CreateStacks();
        }

        public List<Container> PlaceContainers(List<Container> containers)
        {
            foreach (var container in containers)
            {
                if (container.Type == ContainerType.Valuable)
                {
                    if (OrderStacksByWeightOnBottom(GetFrontAndRearRows()).First().TryAddContainer(container))
                    {
                        containers.Remove(container);
                    }
                    else
                    {
                        _previousContainers.Add(container);
                    }
                }
            }
            return containers;
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

        private List<Stack> GetFrontAndRearRows()
        {
            return Stacks.Where(s => s.LengthCoordinates == 1 || s.LengthCoordinates == Length).ToList();
        }

        private bool IsShipBalanced()
        {
            return GetLeftWeight(GetLeftMaxRow()) == GetRightWeight(GetRightMinRow());
        }

        private List<Stack> OrderStacksByWeightOnBottom(List<Stack> stacks)
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

        private List<Container> OrderContainers(List<Container> containers)
        {
            return containers.OrderBy(c => c.Type).ThenByDescending(c => c.Weight).ToList();
        }

        private int WhatIs50PercentWeight()
        {
            return _maxWeightOfStack * Width * Length;
        }
    }
}
