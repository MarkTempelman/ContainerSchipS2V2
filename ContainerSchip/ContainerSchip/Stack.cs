﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchip
{
    public class Stack
    {
        public List<Container> Containers { get; private set; }
        public int WidthCoordinates { get; }
        public int LengthCoordinates { get; }
        private static readonly int _maxWeightOnBottom = 120000;

        public Stack(int widthCoordinates, int lengthCoordinates)
        {
            WidthCoordinates = widthCoordinates;
            LengthCoordinates = lengthCoordinates;
        }

        public bool TryAddContainer(Container container)
        {
            
            if (IsContainerTooHeavyToAdd(container))
            {
                return false;
            }

            if (DoesStackContainValuable())
            {
                if (container.Type == ContainerType.Valuable)
                {
                    return false;
                }

                Containers.Add(container);
                MoveValuableToTop();
                return true;
            }
            Containers.Add(container);
            return true;
        }

        public int GetWeightOnBottomContainer()
        {
            if (Containers.Count < 1)
            {
                return 0;
            }

            return Containers.Skip(1).Sum(c => c.Weight);
        }

        private bool IsContainerTooHeavyToAdd(Container container)
        {
            return GetWeightOnBottomContainer() + container.Weight > _maxWeightOnBottom;
        }

        public bool DoesStackContainValuable()
        {
            return Containers.Any(c => c.Type == ContainerType.Valuable);
        }

        public int GetTotalWeight()
        {
            return Containers.Sum(c => c.Weight);
        }

        private void MoveValuableToTop()
        {
            Container vContainer = Containers.First(c => c.Type == ContainerType.Valuable);
            Containers = Containers.Where(c => c.Type != ContainerType.Valuable).ToList();
            Containers.Add(vContainer);
        }
    }
}
