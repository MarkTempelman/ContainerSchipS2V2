using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using ContainerSchip.ContainerTypes;

namespace ContainerSchip
{
    class Program
    {
        static void Main(string[] args)
        {
            Ship ship = new Ship(3, 3);
            Visualizer visualizer = new Visualizer();

            List<IContainer> containers = new List<IContainer>();

            for (int i = 0; i < 6; i++)
            {
                containers.Add(new ValuableContainer(30000));
            }

            for (int i = 0; i < 12; i++)
            {
                containers.Add(new CooledContainer(30000));
            }

            for (int i = 0; i < 27; i++)
            {
                containers.Add(new RegularContainer(30000));
            }

            ship.PlaceContainers(containers);

            System.Diagnostics.Process.Start(visualizer.GetUrl(ship));
        }
    }
}
