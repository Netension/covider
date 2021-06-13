using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Netension.Covider.Application.Clients
{
    internal class CouchDbStorage : IStorage
    {
        private readonly HttpClient _client;

        public CouchDbStorage(HttpClient client)
        {
            _client = client;
        }

        public Task<bool> CheckApplicationExistsAsync(string name, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateApplicationAsync(string name, CancellationToken cancellationToken)
        {
            await _client.PutAsync(name, JsonContent.Create(new object()), cancellationToken);
        }

        public async Task<IEnumerable<string>> GetApplicationsAsync(CancellationToken cancellationToken)
        {
            return await _client.GetFromJsonAsync<IEnumerable<string>>("_all_dbs", cancellationToken);
        }
    }
}
