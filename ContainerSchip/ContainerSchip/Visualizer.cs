using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
