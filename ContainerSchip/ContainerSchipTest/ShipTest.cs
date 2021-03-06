﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContainerSchip;
using ContainerSchip.ContainerTypes;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
            Assert.AreEqual(ContainerType.Regular, ship.Stacks.First().GetContainerTypeOrder().First());
            Assert.AreEqual(76000, ship.GetCurrentShipWeight());
        }

        [Test]
        public void PlaceContainers_Place2RegularOn1x1_ContainersPlaced()
        {
            ship = new Ship(1, 1);

            Assert.AreEqual(0, ship.PlaceContainers(new List<IContainer>{rContainer, new RegularContainer(4000)}).Count);
            Assert.AreEqual(ContainerType.Regular, ship.Stacks.First().GetContainerTypeOrder().First());
        }

        [Test]
        public void PlaceContainers_Place2RegularOn2x1_ContainersPlaced()
        {
            ship = new Ship(2 , 1);

            Assert.AreEqual(0, ship.PlaceContainers(new List<IContainer>{new RegularContainer(4000), new RegularContainer(4000)}).Count);
            Assert.AreEqual(ContainerType.Regular, ship.Stacks.First(s => s.WidthCoordinates == 1).GetContainerTypeOrder().First());
            Assert.AreEqual(ContainerType.Regular, ship.Stacks.First(s => s.WidthCoordinates == 2).GetContainerTypeOrder().First());
        }

        [Test]
        public void PlaceContainers_1R1C1VOn1x3_ContainersPlacedAddedEmptyContainerToReachMinWeight()
        {
            ship = new Ship(1, 3);
            
            Assert.AreEqual(0, ship.PlaceContainers(new List<IContainer>{rContainer, vContainer, cContainer}).Count);
            Assert.AreEqual(ContainerType.Cooled, ship.Stacks.First(s => s.LengthCoordinates == 1).GetContainerTypeOrder().First());
            Assert.AreEqual(ContainerType.Regular, ship.Stacks.First(s => s.LengthCoordinates == 2).GetContainerTypeOrder().First());
            Assert.AreEqual(ContainerType.Valuable, ship.Stacks.First(s => s.LengthCoordinates == 3).GetContainerTypeOrder().Last());
            Assert.LessOrEqual(225000, ship.GetCurrentShipWeight());
        }

        [Test]
        public void PlaceContainers_1R_PlacedAdditionalContainersToReachMinWeight()
        {
            ship = new Ship(1,1);
            Assert.AreEqual(0, ship.PlaceContainers(new List<IContainer> {rContainer}).Count);
            Assert.LessOrEqual(75000, ship.GetCurrentShipWeight());
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

        [Test]
        public void PlaceContainers_6V12C27ROn3x3_AllContainersPlaced()
        {
            ship = new Ship(3, 3);

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

            Assert.AreEqual(0, ship.PlaceContainers(containers).Count);
            Assert.AreEqual(1350000, ship.GetCurrentShipWeight());
            Assert.AreEqual(null, ship.Stacks.Where(s => s.LengthCoordinates == 2).FirstOrDefault(s => s.DoesStackContainValuable()));
            Assert.AreEqual(null, ship.Stacks.Where(s => s.LengthCoordinates > 1).FirstOrDefault(s => s.DoesStackContainCooled()));
        }

        [Test]
        public void PlaceContainers__AllContainersPlaced()
        {
            ship = new Ship(3, 3);

            List<IContainer> containers = new List<IContainer>();

            for (int i = 0; i < 3; i++)
            {
                containers.Add(new ValuableContainer(25000));
            }

            containers.Add(new ValuableContainer(21000));

            for (int i = 0; i < 2; i++)
            {
                containers.Add(new ValuableContainer(26000));
            }

            for (int i = 0; i < 7; i++)
            {
                containers.Add(new CooledContainer(13000));
            }

            for (int i = 0; i < 13; i++)
            {
                containers.Add(new CooledContainer(4000));
            }

            for (int i = 0; i < 7; i++)
            {
                containers.Add(new CooledContainer(7500));
            }

            for (int i = 0; i < 17; i++)
            {
                containers.Add(new RegularContainer(6000));
            }

            for (int i = 0; i < 13; i++)
            {
                containers.Add(new RegularContainer(17000));
            }

            for (int i = 0; i < 11; i++)
            {
                containers.Add(new RegularContainer(23000));
            }

            for (int i = 0; i < 11; i++)
            {
                containers.Add(new RegularContainer(29000));
            }

            Assert.AreEqual(0, ship.PlaceContainers(containers).Count);
            Assert.AreEqual(1238500, ship.GetCurrentShipWeight());
        }
    } 
}
