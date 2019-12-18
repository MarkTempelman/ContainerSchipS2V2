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
            Visualizer visualizer = new Visualizer();
            while (true)
            {
                visualizer.VisualizeRandomShip();

                Console.ReadLine();
            }
        }
    }
}
