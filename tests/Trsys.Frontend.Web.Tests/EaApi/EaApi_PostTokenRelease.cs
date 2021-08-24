using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Trsys.Frontend.Web.Tests.EaApi
{
    [TestClass]
    public class EaApi_PostTokenRelease
    {
        private WebApplicationFactory<Startup> _factory;

        [TestInitialize]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [TestCleanup]
        public void Teardown()
        {
            _factory.Dispose();
        }

        [TestMethod]
        public async Task ValidToken_ReturnSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsync("/api/token/TOKEN/release", "SECRETKEY", "Publisher");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
