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

            Assert.AreEqual(0, ship.PlaceContainers(new List<IContainer>{rContainer, new RegularContainer(4000)}));
            Assert.AreEqual(rContainer.Weight + 4000, ship.Stacks.First().GetTotalWeight());
        }
    }
}
