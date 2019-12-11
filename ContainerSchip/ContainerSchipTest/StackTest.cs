using System.Collections;
using System.Reflection.Emit;
using NUnit.Framework;
using ContainerSchip;
using ContainerSchip.ContainerTypes;
using Stack = ContainerSchip.Stack;

namespace Tests
{
    public class Tests
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
    }
}