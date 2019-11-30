using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchip
{
    public class Stack
    {
        public List<Container> Containers { get; }
        public int LengthCoordinates { get; }
        public int WidthCoordinates { get; }
        private static int _maxWeightOnBottom = 120000;

        public Stack(int lengthCoordinates, int widthCoordinates)
        {
            LengthCoordinates = lengthCoordinates;
            WidthCoordinates = widthCoordinates;
        }

        public bool TryAddContainer(Container container)
        {
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
            return GetWeightOnBottomContainer() + container.Weight <= _maxWeightOnBottom;
        }

        public bool DoesStackContainValuable()
        {
            return
        }
    }
}
