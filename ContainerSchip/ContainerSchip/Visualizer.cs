using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerSchip.ContainerTypes;

namespace ContainerSchip
{
    public class Visualizer
    {
        public string GetUrl(Ship ship)
        {
            string url = "https://i872272core.venus.fhict.nl/ContainerVisualizer/index.html?length=";

            url += ship.Length.ToString();
            url += "&width=";
            url += ship.Width.ToString();
            url += "&stacks=";

            url = AddContainersToString(url, ship);
            url = AddContainerWeightToString(url, ship);

            return url;
        }

        private string AddContainersToString(string url, Ship ship)
        {
            for (int i = 1; i <= ship.Width; i++)
            {
                var row = ship.Stacks.Where(s => s.WidthCoordinates == i).OrderBy(s => s.LengthCoordinates).ToList();
                foreach (var stack in row)
                {
                    foreach (var container in stack.GetContainers())
                    {
                        if (container.Type == ContainerType.Valuable)
                        {
                            url += 2.ToString();
                        }
                        else if (container.Type == ContainerType.Cooled)
                        {
                            url += 3.ToString();
                        }
                        else
                        {
                            url += 1.ToString();
                        }
                    }
                    url += ",";
                }
                url = url.TrimEnd(',');
                url += "/";
            }
            url = url.TrimEnd('/');
            return url;
        }

        private string AddContainerWeightToString(string url, Ship ship)
        {
            url += "&weights=";

            for (int i = 1; i <= ship.Width; i++)
            {
                var row = ship.Stacks.Where(s => s.WidthCoordinates == i).OrderBy(s => s.LengthCoordinates).ToList();
                foreach (var stack in row)
                {
                    foreach (var container in stack.GetContainers())
                    {
                        url += (container.Weight / 1000).ToString();
                        url += "-";
                    }
                    url = url.TrimEnd('-');
                    url += ",";
                }
                url = url.TrimEnd(',');
                url += "/";
            }
            url = url.TrimEnd('/');
            return url;
        }

        public void VisualizeRandomShip()
        {
            Ship ship = GenerateRandomShip();
            List<IContainer> containers = GenerateRandomContainersForShip(ship);
            ship.PlaceContainers(containers);
            
            System.Diagnostics.Process.Start(GetUrl(ship));
        }

        public Ship GenerateRandomShip()
        {
            Random rnd = new Random();
            int width = rnd.Next(2, 6);
            int length = rnd.Next(2, 10);

            return new Ship(width, length);
        }

        public List<IContainer> GenerateRandomContainersForShip(Ship ship)
        {
            List<IContainer> containers = new List<IContainer>();
            Random rnd = new Random();
            int amountOfValuable = rnd.Next(1, ship.Width * 2);
            int amountOfCooled = rnd.Next(1, ship.Width * 9);
            int amountOfRegular = rnd.Next((ship.Width * (ship.Length - 2) * 11) + ship.Width * 9);

            for (int i = 0; i < amountOfValuable; i++)
            {
                containers.Add(new ValuableContainer(rnd.Next(4, 30) * 1000));
            }
            for (int i = 0; i < amountOfCooled; i++)
            {
                containers.Add(new CooledContainer(rnd.Next(4, 30) * 1000));
            }
            for (int i = 0; i < amountOfRegular; i++)
            {
                containers.Add(new RegularContainer(rnd.Next(4, 30) * 1000));
            }

            return containers;
        }
    }
}