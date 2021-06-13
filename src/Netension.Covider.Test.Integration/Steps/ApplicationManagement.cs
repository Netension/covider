using FluentAssertions;
using Moq;
using Netension.Covider.Test.Integration.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace Netension.Covider.Test.Integration.Steps
{
    [Binding]
    public sealed class ApplicationManagement : IClassFixture<TestApplicationFactory>
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestApplicationFactory _factory;
        private System.Net.Http.HttpResponseMessage _response;

        public ApplicationManagement(ScenarioContext scenarioContext, TestApplicationFactory factory)
        {
            _scenarioContext = scenarioContext;
            _factory = factory;
        }

        [Given("I do not have any application")]
        public void IDoNotHaveApplication()
        {
            _factory.StorageMock.Setup(s => s.GetApplicationsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Enumerable.Empty<string>());
        }

        [Given("I have (.*) application")]
        public void IHaveApplication(string name)
        {
            _factory.StorageMock.Setup(s => s.GetApplicationsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<string> { name });
        }

        [When("I call /api/application/(.*) POST action")]
        public async Task CreateApplication(string name)
        {
            var client = _factory.CreateClient();

            _response = await client.PostAsJsonAsync($"api/Application/{name}", new object(), new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
        }

        [Then("(.*) should be created")]
        public void ApplicationCreated(string name)
        {
            _factory.StorageMock.Verify(s => s.CreateApplicationAsync(It.Is<string>(n => n.Equals(name)), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Then("Response should be 400")]
        public void ResponseShouldBe400()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
