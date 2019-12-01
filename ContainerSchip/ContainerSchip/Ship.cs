using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSchip
{
    public class Ship
    {
        public int Length { get; }
        public int Width { get; }
        private List<Stack> Stacks { get; } = new List<Stack>();

        public Ship(int length, int width)
        {
            Length = length;
            Width = width;
            CreateStacks();
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

        private bool IsShipWidthEven()
        {
            return Width % 2 == 0;
        }

        private int GetCentreRow()
        {
            double centre = (double) Width / 2;
            return Convert.ToInt32(Math.Ceiling(centre));
        }

        private int GetLeftMaxRow()
        {
            if (IsShipWidthEven())
            {
                return Width / 2;
            }

            return GetCentreRow() - 1;
        }

        private int GetRightMinRow()
        {
            if (IsShipWidthEven())
            {
                return (Width / 2 + 1);
            }

            return GetCentreRow() + 1;
        }

        private int GetLeftWeight(int leftMax)
        {
            return Stacks.Where(s => s.WidthCoordinates <= leftMax).Sum(s => s.GetTotalWeight());
        }

        private int GetRightWeight(int rightMin)
        {
            return Stacks.Where(s => s.WidthCoordinates >= rightMin).Sum(s => s.GetTotalWeight());
        }
    }
}
