using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Trsys.Frontend.Tests
{
    [TestClass]
    public class EaApi_GetOrders
    {
        private WebApplicationFactory<Startup> _factory;

        [TestInitialize]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Startup>();
            _factory.ClientOptions.AllowAutoRedirect = true;
        }

        [TestCleanup]
        public void Teardown()
        {
            _factory.Dispose();
        }

        [TestMethod]
        public async Task ReturnSuccessAndCorrectContentType()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/orders");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("text/plain; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}