using Core;
using Core.Cache;
using Data.Repository;
using Model;
using Moq;
using Xunit;

namespace UnitTest
{
    public class ConfigurationBaseTest
    {
        private ConfigurationBase _configurationBase;

        public ConfigurationBaseTest()
        {
            var mockCacher = new Mock<ICacher>();
            var mockDataHelper = new Mock<IConfigurationRepository>();
            _configurationBase = new ConfigurationBase(mockCacher.Object, mockDataHelper.Object,"");
        }

        [Fact]
        public void Given_intObject_When_call_ConvertToType_Then_Return_Int()
        {
            //Arrange
            var configObject = new ConfigurationObject()
            {
                Type = "Int",
                Value = "1234"
            };
            //Act
            var sut = _configurationBase.ConvertToType<int>(configObject);

            //Assert
            Assert.Equal(1234, sut);
        }
    }
}
