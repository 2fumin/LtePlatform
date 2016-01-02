using System.Linq;
using System.Reflection;
using Abp.Localization.Dictionaries.Xml;
using Shouldly;
using Xunit;

namespace Abp.Tests.Localization
{
    public class XmlEmbeddedFileLocalizationDictionaryProvider_Tests
    {
        private readonly XmlEmbeddedFileLocalizationDictionaryProvider _dictionaryProvider;

        public XmlEmbeddedFileLocalizationDictionaryProvider_Tests()
        {
            _dictionaryProvider = new XmlEmbeddedFileLocalizationDictionaryProvider(
                Assembly.GetExecutingAssembly(),
                "Abp.Tests.Localization.TestXmlFiles"
                );

            _dictionaryProvider.Initialize("Test");
        }

        [Fact]
        public void Test_Assmebly()
        {
            var assmebly = Assembly.GetExecutingAssembly();
            assmebly.FullName.ShouldBe("Abp.Tests, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            var names = assmebly.GetManifestResourceNames();
            Assert.Equal(names.Length, 1);
            Assert.Equal(names[0], "");
        }

        [Fact]
        public void Should_Get_Dictionaries()
        {
            var dictionaries = _dictionaryProvider.Dictionaries.Values.ToList();
            
            dictionaries.Count.ShouldBe(2);

            var enDict = dictionaries.FirstOrDefault(d => d.CultureInfo.Name == "en");
            enDict.ShouldNotBe(null);
            enDict.ShouldBe(_dictionaryProvider.DefaultDictionary);
            enDict["hello"].ShouldBe("Hello");
            
            var trDict = dictionaries.FirstOrDefault(d => d.CultureInfo.Name == "tr");
            trDict.ShouldNotBe(null);
            trDict["hello"].ShouldBe("Merhaba");
        }
    }
}
