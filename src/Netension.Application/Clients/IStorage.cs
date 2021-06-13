using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Netension.Covider.Application.Clients
{
    public interface IStorage
    {
        Task<IEnumerable<string>> GetApplicationsAsync(CancellationToken cancellationToken);
        Task CreateApplicationAsync(string name, CancellationToken cancellationToken);
        Task<bool> CheckApplicationExistsAsync(string name, CancellationToken cancellationToken);
    }
}
