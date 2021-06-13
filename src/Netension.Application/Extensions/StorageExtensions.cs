using Netension.Covider.Application.Clients;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Netension.Covider.Application.Extensions
{
    public static class StorageExtensions
    {
        public static async Task<bool> IsApplicationExistsAsync(this IApplicationRepository storage, string name, CancellationToken cancellationToken)
        {
            var applications = await storage.GetAsync(cancellationToken);
            return applications.Contains(name);
        }
    }
}
