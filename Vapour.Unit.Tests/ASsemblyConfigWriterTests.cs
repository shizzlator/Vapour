using System.Collections.Generic;
using System.IO;
using Moq;
using NUnit.Framework;
using Vapour.Domain;
using Vapour.Domain.Config;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;
using Vapour.Domain.TestRunner;

namespace Vapour.Unit.Tests
{
    [TestFixture]
    public class AssemblyConfigWriterTests
    {
        private ProjectConfiguration _projectConfig;
        private Mock<IStreamWriterWrapper> _fakeStreamWriter;
        private Mock<IProjectConfigurationRepository> _fakeProjectConfigRepository;
        private Mock<IConfig> _fakeConfig;
        private AssemblyConfigWriter _assemblyConfigWriter;

        [SetUp]
        public void SetUp()
        {
            _projectConfig = new ProjectConfiguration
            {
                ProjectName = "AppYours",
                AssemblyName = "AppYours.Smoke.Tests",
                Environment = "TeamDave",
                TestDescription = "Smoke",
                ConfigurationCollection = new Dictionary<string, string>
                {
	                { "baseUrl", "www.something.com" }, 
					{ "somekey", "somevalue" }
                }
            };

            _fakeStreamWriter = new Mock<IStreamWriterWrapper>();
            _fakeProjectConfigRepository = new Mock<IProjectConfigurationRepository>();
            _fakeConfig = new Mock<IConfig>();
            _assemblyConfigWriter = new AssemblyConfigWriter(_fakeStreamWriter.Object, _fakeProjectConfigRepository.Object, _fakeConfig.Object);

            _fakeConfig.Setup(x => x.AssemblyStorePath).Returns("D:\\Vapour\\Projects\\");
            _fakeProjectConfigRepository.Setup(x => x.Get(_projectConfig)).Returns(_projectConfig);
        }

        [Test]
		public void WriteConfigFor_should_write_file_to_configured_path()
        {
			// given + when
			_assemblyConfigWriter.WriteConfigFor(_projectConfig);

			// then
			_fakeStreamWriter.Verify(x => x.CreateFile("D:\\Vapour\\Projects\\AppYours\\Smoke\\TeamDave\\AppYours.Smoke.Tests.dll.config"), Times.Once);
        }

        [Test]
		public void WriteConfigFor_should_writeout_appsettings_from_given_project_configuration_object()
        {
			// given + when
            _assemblyConfigWriter.WriteConfigFor(_projectConfig);

			// then
            _fakeStreamWriter.Verify(x => x.WriteLine(@"<add key=""baseUrl"" value=""www.something.com"" />"), Times.Once);
            _fakeStreamWriter.Verify(x => x.WriteLine(@"<add key=""somekey"" value=""somevalue"" />"), Times.Once);
        }

        [Test]
		public void WriteConfigFor_should_write_out_beginning_and_end_of_configfile()
        {
			// given + when
            _assemblyConfigWriter.WriteConfigFor(_projectConfig);

			// then
            _fakeStreamWriter.Verify(x => x.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8""?><configuration><appSettings>"), Times.Once);
            _fakeStreamWriter.Verify(x => x.WriteLine(@"</appSettings></configuration>"), Times.Once);
        }
    }
}