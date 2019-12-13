using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using NUnit.Framework;
using ContainerSchip;
using ContainerSchip.ContainerTypes;
using Stack = ContainerSchip.Stack;

namespace ContainerSchipTest
{
    public class StackTests
    {
        private Stack stack;
        private IContainer rContainer;
        private IContainer cContainer;
        private IContainer vContainer;
        [SetUp]
        public void Setup()
        {
            stack = new Stack(1, 1);
            rContainer = new RegularContainer(4000);
            cContainer = new CooledContainer(4000);
            vContainer = new ValuableContainer(4000);
        }

        [Test]
        public void TryAddContainer_AddRegular_ContainerAdded()
        {
            stack.TryAddContainer(rContainer);
            Assert.AreEqual(rContainer.Weight, stack.GetTotalWeight());
        }

        [Test]
        public void TryAddContainer_AddCooled_ContainerAdded()
        {
            stack.TryAddContainer(cContainer);
            Assert.AreEqual(cContainer.Weight, stack.GetTotalWeight());
        }

        [Test]
        public void TryAddContainer_AddValuable_ContainerAdded()
        {
            stack.TryAddContainer(vContainer);
            Assert.AreEqual(cContainer.Weight, stack.GetTotalWeight());
        }

        [Test]
        public void TryAddContainer_AddValuableToStackWithValuable_ReturnsFalseContainerNotAdded()
        {
            IContainer v2Container = new ValuableContainer(4000);
            stack.TryAddContainer(vContainer);

            Assert.False(stack.TryAddContainer(v2Container));
        }

        [Test]
        public void TryAddContainer_AddRegularToFullStack_ReturnFalse()
        {
            for (int i = 0; i < 5; i++)
            {
                stack.TryAddContainer(new RegularContainer(30000));
            }

            Assert.False(stack.TryAddContainer(rContainer));
        }

        [Test]
        public void TryAddContainer_AddRegularToValuable_ValuableMovedToTop()
        {
            stack.TryAddContainer(vContainer);
            stack.TryAddContainer(rContainer);

            List<ContainerType> expected = new List<ContainerType>()
            {
                ContainerType.Regular,
                ContainerType.Valuable
            };

            var actual = stack.GetContainerTypeOrder();

            Assert.AreEqual(expected, actual);
        }
    }
}