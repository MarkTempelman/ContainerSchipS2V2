using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchip
{
    public class Stack
    {
        private List<IContainer> _containers = new List<IContainer>();
        public int WidthCoordinates { get; }
        public int LengthCoordinates { get; }
        private static readonly int _maxWeightOnBottom = 120000;

        public Stack(int widthCoordinates, int lengthCoordinates)
        {
            WidthCoordinates = widthCoordinates;
            LengthCoordinates = lengthCoordinates;
        }

        public bool TryAddContainer(IContainer container)
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

                _containers.Add(container);
                MoveValuableToTop();
                return true;
            }
            _containers.Add(container);
            return true;
        }

        public int GetWeightOnBottomContainer()
        {
            if (_containers.Count < 2)
            {
                return 0;
            }

            return _containers.Skip(1).Sum(c => c.Weight);
        }

        private bool IsContainerTooHeavyToAdd(IContainer container)
        {
            return GetWeightOnBottomContainer() + container.Weight > _maxWeightOnBottom;
        }

        public bool DoesStackContainValuable()
        {
            return _containers.Any(c => c.Type == ContainerType.Valuable);
        }

        public int GetTotalWeight()
        {
            return _containers.Sum(c => c.Weight);
        }

        private void MoveValuableToTop()
        {
            IContainer vContainer = _containers.First(c => c.Type == ContainerType.Valuable);
            _containers = _containers.Where(c => c.Type != ContainerType.Valuable).ToList();
            _containers.Add(vContainer);
        }
    }
}
