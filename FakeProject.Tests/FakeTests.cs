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
    }
}
