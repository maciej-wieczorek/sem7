using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SumUpThreeValuesTest()
        {
            Assert.AreEqual(10, 5+5);
        }
    }
}