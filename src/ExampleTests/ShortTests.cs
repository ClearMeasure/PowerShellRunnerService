using System.Threading;
using NUnit.Framework;

namespace ExampleTests
{
    [TestFixture]
    public class ShortTests
    {
        [Test]
        public void ShortTestOne()
        {
            Assert.AreEqual(1,1);
        }

        [Test]
        public void ShortTestTwo()
        {
            Assert.AreEqual(1, 1);
        }

        [Test]
        public void ShortTestThree()
        {
            Assert.AreEqual(1,0);
        }
    }
}
