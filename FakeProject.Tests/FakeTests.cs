using NUnit.Framework;

namespace FakeProject.Tests
{
    [TestFixture]
    public class FakeTests
    {
        [Test]
        public void ShouldPass()
        {
            Assert.That(true, Is.True);
        }

        [Test]
        public void ShouldFail()
        {
            Assert.That(false, Is.True);
        }

        [Test]
        public void ShouldFailWithNull()
        {
            Assert.That(null, Is.Not.Null);
        }
    }
}
