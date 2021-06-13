using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Netension.Covider.Application.Clients
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<string>> GetAsync(CancellationToken cancellationToken);
        Task SaveAsync(string name, CancellationToken cancellationToken);
        Task DeleteAsync(string name, CancellationToken cancellationToken);
    }
}
