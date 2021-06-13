using Microsoft.Extensions.Logging;
using Netension.Covider.Application.Clients;
using Netension.Request.Handlers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Netension.Covider.Application.QueryHandlers
{
    public class GetApplicationsQueryHandler : QueryHandler<GetApplicationsQuery, IEnumerable<string>>
    {
        private readonly IApplicationRepository _repository;

        public GetApplicationsQueryHandler(IApplicationRepository repository, ILogger<GetApplicationsQueryHandler> logger)
            : base(logger)
        {
            _repository = repository;
        }

        public override async Task<IEnumerable<string>> HandleAsync(GetApplicationsQuery query, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync(cancellationToken);
        }
    }
}
