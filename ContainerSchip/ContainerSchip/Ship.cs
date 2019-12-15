using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerSchip.ContainerTypes;

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
            while (true)
            {
                currentContainers = SortContainers(currentContainers);
                List<IContainer> newContainers = new List<IContainer>();
                foreach (var container in currentContainers)
                {
                    if (!TryPlaceContainer(container))
                    {
                        newContainers.Add(container);
                    }
                }

                if (HasContainerListChanged(newContainers) && newContainers.Count > 0)
                {
                    _previousContainers = newContainers;
                    currentContainers = newContainers;
                    continue;
                }

                if (GetCurrentShipWeight() < WhatIsTotalMaxWeight() / 2)
                {
                    TryPlaceContainer(new RegularContainer(4000));
                    currentContainers = newContainers;
                    continue;
                }

                return newContainers;
            }
        }

        private bool TryPlaceContainer(IContainer container)
        {
            return container.TryPlaceOnBalancedShip(this);
        }

        public bool HasContainerListChanged(List<IContainer> containers)
        {
            return _previousContainers.Count != containers.Count;
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

        public List<Stack> OrderStacksByWeightOnBottom(List<Stack> stacks)
        {
            return stacks.OrderBy(c => c.GetWeightOnBottomContainer()).ToList();
        }

        private List<IContainer> SortContainers(List<IContainer> containers)
        {
            return containers.OrderBy(c => c.Type).ThenByDescending(c => c.Weight).ToList();
        }

        private int WhatIsTotalMaxWeight()
        {
            return _maxWeightOfStack * Width * Length;
        }

        public int GetCurrentShipWeight()
        {
            return Stacks.Sum(stack => stack.GetTotalWeight());
        }
    }
}
