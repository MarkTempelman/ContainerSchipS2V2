using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchip
{
    public interface IContainer
    {
        int Weight { get; }
        ContainerType Type { get; }

        bool TryPlaceOnBalancedShip(Ship ship);
        bool TryPlaceOnImbalancedShip(Ship ship);
    }
}
