using FluentAssertions;
using Moq;
using Netension.Covider.Test.Integration.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        private HttpResponseMessage _response;

        public ApplicationManagement(ScenarioContext scenarioContext, TestApplicationFactory factory)
        {
            _scenarioContext = scenarioContext;
            _factory = factory; 
        }

        [Given("I do not have any application")]
        public void IDoNotHaveApplication()
        {
            _factory.StorageMock.Setup(s => s.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Enumerable.Empty<string>());
        }

        [Given("I have (.*) application")]
        public void IHaveApplication(string name)
        {
            _factory.StorageMock.Setup(s => s.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<string> { name });
        }

        [Given("I have the following applications")]
        public void IHaveApplications(Table applications)
        {
            _factory.StorageMock.Setup(s => s.GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(GetApplications(applications));
        }

        [When("I call /api/application/(.*) POST action")]
        public async Task CreateApplication(string name)
        {
            var client = _factory.CreateClient();

            _response = await client.PostAsJsonAsync($"api/Application/{name}", new object(), new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
        }

        [When("I call /api/application/(.*) DELETE action")]
        public async Task DeleteApplication(string name)
        {
            var client = _factory.CreateClient();

            _response = await client.DeleteAsync($"api/Application/{name}", new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
        }

        [When("I call /api/application GET action")]
        public async Task GetApplications()
        {
            var client = _factory.CreateClient();

            _response = await client.GetAsync($"api/Application", new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token);
        }

        [Then("(.*) should be created")]
        public void ApplicationCreated(string name)
        {
            _factory.StorageMock.Verify(s => s.SaveAsync(It.Is<string>(n => n.Equals(name)), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Then("Response should be 400")]
        public void ResponseShouldBe400()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Then("(.*) should be deleted")]
        public void ShouldBeDeleted(string name)
        {
            _factory.StorageMock.Verify(s => s.DeleteAsync(It.Is<string>(n => n.Equals(name)), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Then("Should be response with the following applications")]
        public async Task ShouldBeDeleted(Table applications)
        {
            var response = await _response.Content.ReadFromJsonAsync<IEnumerable<string>>();
            response.Should().Contain(GetApplications(applications));
        }

        private IEnumerable<string> GetApplications(Table applications)
        {
            var response = new List<string>();
            foreach (var row in applications.Rows)
            {
                response.Add(row["Name"]);
            }

            return response;
        }
    }
}
