using System.Linq;
using NUnit.Framework;
using Vapour.Domain;
using Vapour.Domain.DataAccess;

namespace Vapour.Integration.Tests
{
    [TestFixture]
    public class AssemblyDetailsRepositoryTests
    {
        private DatabaseSession _databaseSession;
        private AssemblyDetailsRepository _assemblyDetailsRepository;
        private AssemblyDetails _assemblyDetails;

        [SetUp]
        public void FixtureSetUp()
        {
            _assemblyDetails = new AssemblyDetails
            {
                AppName = "MyBadApp",
                AssemblyName = "MyBadApp.Smoke.Tests",
                TestDescription = "Smoke"
            };

            _databaseSession = new DatabaseSession();
            _assemblyDetailsRepository = new AssemblyDetailsRepository(_databaseSession);
        }

        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
            _databaseSession.GetCollection<AssemblyDetails>(VapourCollections.AssemblyDetails).RemoveAll();
        }

        [Test]
        public void ShouldInsertAndRetrieveAssemblyDetails()
        {
            var queryObject = new { AppName = _assemblyDetails.AppName };

            _assemblyDetailsRepository.Save(_assemblyDetails);

            var result = _databaseSession.RunQuery<AssemblyDetails>(queryObject, VapourCollections.AssemblyDetails);

            Assert.That(result.FirstOrDefault().AssemblyName, Is.EqualTo(_assemblyDetails.AssemblyName));
        }
    }
}