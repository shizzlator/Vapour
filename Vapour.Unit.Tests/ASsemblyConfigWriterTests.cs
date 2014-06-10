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

namespace Vapour.Unit.Tests
{
	[TestFixture]
	public class AssemblyConfigWriterTests
	{
		private const string _xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
	<appSettings>
		<!-- A comment-->
		<add key=""Host"" value=""localhost"" />
		<add key=""Port"" value=""5051"" />

		<add key=""Folder"" value=""d:\mailroot\pickup"" />

		<!-- Urls for webdriver tests -->
		<add key=""Url"" value=""http://localhost:8072/bar.asmx"" />
		<add key=""OtherUrl"" value=""http://localhost:8001/"" />
		<add key=""CurrentUrl"" value=""http://localhost:8001/foo.aspx"" />
	</appSettings>

	<system.serviceModel><thisShouldExist/></system.serviceModel></configuration>";

		private ProjectConfiguration _projectConfig;
		private Mock<IProjectConfigurationRepository> _fakeProjectConfigRepository;
		private Mock<IConfig> _fakeConfig;

		[SetUp]
		public void SetUp()
		{
			_projectConfig = new ProjectConfiguration();

			_fakeProjectConfigRepository = new Mock<IProjectConfigurationRepository>();
			_fakeConfig = new Mock<IConfig>();
		}

		[Test]
		public void UpdateKeys_should_keep_existing_elements()
	    {
			// given
			var assemblyConfigWriter = new AssemblyConfigWriter(_fakeProjectConfigRepository.Object, _fakeConfig.Object);

			StringReader reader = new StringReader(_xml);
		    XDocument document = XDocument.Load(reader);

			var appSettings = new Dictionary<string, string>();

			// when
			assemblyConfigWriter.UpdateKeys(document, appSettings);

			// then
			XElement element = document.Root.Descendants().FirstOrDefault(x => x.Name.LocalName == "thisShouldExist");
			Assert.That(element, Is.Not.Null);
	    }

		[Test]
		public void UpdateKeys_should_replace_appsetting_values()
		{
			// given
			var assemblyConfigWriter = new AssemblyConfigWriter(_fakeProjectConfigRepository.Object, _fakeConfig.Object);

			StringReader reader = new StringReader(_xml);
			XDocument document = XDocument.Load(reader);

			var appSettings = new Dictionary<string, string>()
		    {
			    { "Folder", @"new folder"},
				{ "CurrentUrl", "A new url" }
		    };

			// when
			assemblyConfigWriter.UpdateKeys(document, appSettings);

			// then
			IEnumerable<XElement> elements = document.Root.Descendants().Where(x => x.Name.LocalName == "add");

			XElement addElement = elements.FirstOrDefault(x => x.Attribute("key").Value == "Folder");
			Assert.That(addElement.Attribute("value").Value, Is.EqualTo("new folder"));

			addElement = elements.FirstOrDefault(x => x.Attribute("key").Value == "CurrentUrl");
			Assert.That(addElement.Attribute("value").Value, Is.EqualTo("A new url"));
		}
	}
}