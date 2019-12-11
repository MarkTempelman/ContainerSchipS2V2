using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerSchip.ContainerTypes;

namespace ContainerSchip
{
    class Program
    {
        static void Main(string[] args)
        {
            Ship ship = new Ship(2,2);
            ship.PlaceContainers(new List<IContainer>()
            {
                new CooledContainer(4000)
            });
        }
    }
}
