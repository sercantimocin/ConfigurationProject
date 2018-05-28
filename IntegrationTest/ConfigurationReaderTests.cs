using System.ComponentModel;
using Core;
using Xunit;

namespace IntegrationTest
{
    [Category("Core")]
    public class ConfigurationReaderTests
    {
        private ConfigurationReader _readerA;
        private ConfigurationReader _readerB;

        public ConfigurationReaderTests()
        {
            _readerA = new ConfigurationReader("SERVICE-A", "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True", 50000);
            _readerB = new ConfigurationReader("SERVICE-B", "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True", 50000);
        }

        [Theory]
        [InlineData("SiteName")]
        public void Given_ServiceA_validkey_When_call_GetValue_Then_Return_String(string key)
        {
            //Arrange

            //Act
            var sut = _readerA.GetValue<string>(key);

            //Assert
            Assert.Equal("Boyner.com.tr", sut);
        }

        [Theory]
        [InlineData("SiteNam")]
        public void Given_ServiceA_invalidkey_When_call_GetValue_Then_Return_Null(string key)
        {
            //Arrange

            //Act
            var sut = _readerA.GetValue<string>(key);

            //Assert
            Assert.Null(sut);
        }

        [Theory]
        [InlineData("IsBasketEnabled")]
        public void Given_ServiceB_validkey_When_call_GetValue_Then_Return_True(string key)
        {
            //Arrange

            //Act
            var sut = _readerB.GetValue<bool>(key);

            //Assert
            Assert.True(sut);
        }

        [Theory]
        [InlineData("SameKey")]
        public void Given_samekey_differentservices_When_call_GetValue_Then_Success(string key)
        {
            //Arrange

            //Act
            var sutA = _readerA.GetValue<int>(key);
            var sutB = _readerB.GetValue<int>(key);

            //Assert
            Assert.Equal(50, sutA);
            Assert.Equal(1, sutB);
        }
    }
}
