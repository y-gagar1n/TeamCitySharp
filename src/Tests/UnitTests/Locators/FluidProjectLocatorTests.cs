using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TeamCitySharp.Locators;

namespace TeamCitySharp.UnitTests
{

    public class FluidProjectLocatorTests
    {

        [TestFixture]
        public class ToStringMethod
        {

            [Test]
            public void ReturnsWithNullId()
            {
                var locator = FluidProjectLocator.WithId(null);
                Assert.AreEqual(string.Empty, locator.ToString());
            }

            [Test]
            public void ReturnsWithEmptyId()
            {
                var locator = FluidProjectLocator.WithId(null);
                Assert.AreEqual(string.Empty, locator.ToString());
            }

            [Test]
            public void ReturnsWithId()
            {
                var locator = FluidProjectLocator.WithId("9999");
                Assert.AreEqual("id:9999", locator.ToString());
            }

            [Test]
            public void ReturnsWithNullName()
            {
                var locator = FluidProjectLocator.WithName(null);
                Assert.AreEqual(string.Empty, locator.ToString());
            }

            [Test]
            public void ReturnsWithEmptyName()
            {
                var locator = FluidProjectLocator.WithName(string.Empty);
                Assert.AreEqual(string.Empty, locator.ToString());
            }

            [Test]
            public void ReturnsWithName()
            {
                var locator = FluidProjectLocator.WithName("PROJECTNAME");
                Assert.AreEqual("name:PROJECTNAME", locator.ToString());
            }

        }

    }

}
