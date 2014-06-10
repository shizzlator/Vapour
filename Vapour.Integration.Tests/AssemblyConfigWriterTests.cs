using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Moq;
using NUnit.Framework;
using Vapour.Domain.Configuration;
using Vapour.Domain.DataAccess;
using Vapour.Domain.Models;
using Vapour.Domain.TestRunner;
using System.Linq;
using System.Xml;

namespace Vapour.Unit.Integration
{
	[TestFixture]
	public class AssemblyConfigWriterTests
	{
		private ProjectConfiguration _projectConfig;
		private Mock<IProjectConfigurationRepository> _fakeProjectConfigRepository;
		private Mock<IConfig> _fakeConfig;
		private string _assemblyPath;

		[SetUp]
		public void SetUp()
		{
			_projectConfig = new ProjectConfiguration()
			{
				ProjectName = "FakeProject",
				AssemblyName = "FakeProject.Tests",
				Environment = "Development",
				TestDescription = "Smoke"
			};
			_fakeProjectConfigRepository = new Mock<IProjectConfigurationRepository>();
			_fakeConfig = new Mock<IConfig>();

			_assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestAssemblies");
			_fakeConfig.Setup(x => x.AssemblyStorePath).Returns(_assemblyPath);
			_fakeProjectConfigRepository.Setup(x => x.Get(_projectConfig)).Returns(_projectConfig);
		}

		[Test]
		public void WriteConfigFor_should_replace_appsetting_values_and_write_to_file()
		{
			// given
			string configPath = _projectConfig.GetAssemblyConfigPathFor(_assemblyPath);
			var assemblyConfigWriter = new AssemblyConfigWriter(_fakeProjectConfigRepository.Object, _fakeConfig.Object);

			var appSettings = new Dictionary<string, string>()
		    {
			    { "PickupFolder", @"new folder"},
				{ "Url", "A new url" }
		    };
			_projectConfig.ConfigurationCollection = appSettings;

			// when
			assemblyConfigWriter.WriteConfigFor(_projectConfig);

			// then
			string xml = File.ReadAllText(configPath);
			Assert.That(xml, Is.StringContaining("new folder"));
			Assert.That(xml, Is.StringContaining("A new url"));
		}
	}
}