using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContainerSchip;
using ContainerSchip.ContainerTypes;
using NUnit.Framework;

namespace ContainerSchipTest
{
    public class ShipTest
    {
        private Ship ship;
        private IContainer rContainer;
        private IContainer cContainer;
        private IContainer vContainer;

        [SetUp]
        public void Setup()
        {
            rContainer = new RegularContainer(4000);
            cContainer = new CooledContainer(4000);
            vContainer = new ValuableContainer(4000);
        }

        [Test]
        public void PlaceContainers_Place1RegularOn1x1_ContainerPlaced()
        {
            ship = new Ship(1, 1);

            Assert.AreEqual(0, ship.PlaceContainers(new List<IContainer>() { rContainer }).Count);
            Assert.AreEqual(rContainer.Weight, ship.Stacks.First().GetTotalWeight());
        }

        [Test]
        public void PlaceContainers_Place2RegularOn1x1_ContainersPlaced()
        {
            ship = new Ship(1, 1);

            Assert.AreEqual(0, ship.PlaceContainers(new List<IContainer>{rContainer, new RegularContainer(4000)}).Count);
            Assert.AreEqual(rContainer.Weight + 4000, ship.Stacks.First().GetTotalWeight());
        }

        [Test]
        public void PlaceContainers_Place2RegularOn2x1_ContainersPlaced()
        {
            ship = new Ship(2 , 1);

            Assert.AreEqual(0, ship.PlaceContainers(new List<IContainer>{new RegularContainer(4000), new RegularContainer(4000)}).Count);
            Assert.AreEqual(4000, ship.Stacks.First(s => s.WidthCoordinates == 1).GetTotalWeight());
            Assert.AreEqual(4000, ship.Stacks.First(s => s.WidthCoordinates == 2).GetTotalWeight());
        }

        [Test]
        public void PlaceContainers_1R1C1VOn1x3_ContainersPlaced()
        {
            ship = new Ship(1, 3);
            
            Assert.AreEqual(0, ship.PlaceContainers(new List<IContainer>{rContainer, vContainer, cContainer}).Count);
            Assert.AreEqual(ContainerType.Cooled, ship.Stacks.First(s => s.LengthCoordinates == 1).GetContainerTypeOrder().First());
            Assert.AreEqual(ContainerType.Regular, ship.Stacks.First(s => s.LengthCoordinates == 2).GetContainerTypeOrder().First());
            Assert.AreEqual(ContainerType.Valuable, ship.Stacks.First(s => s.LengthCoordinates == 3).GetContainerTypeOrder().First());
        }

        [Test]
        public void PlaceContainers_3VOn1x2_1ContainerNotPlaced()
        {
            ship = new Ship(1, 2);

            Assert.AreEqual(1, ship.PlaceContainers(new List<IContainer>
            {
                new ValuableContainer(4000),
                new ValuableContainer(4000),
                new ValuableContainer(4000)
            }).Count);
        }
    }
}
