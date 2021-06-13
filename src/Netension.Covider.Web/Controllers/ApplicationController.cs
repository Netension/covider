using Microsoft.AspNetCore.Mvc;
using Netension.Request.Abstraction.Senders;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Netension.Covider.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ICommandSender _commandSender;
        private readonly IQuerySender _querySender;

        public ApplicationController(ICommandSender commandSender, IQuerySender querySender)
        {
            _commandSender = commandSender;
            _querySender = querySender;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get(CancellationToken cancellationToken)
        {
            return await _querySender.QueryAsync(new GetApplicationsQuery(), cancellationToken);
        }

        [HttpPost("{name:required}")]
        public async Task CreateApplication(string name, CancellationToken cancellationToken)
        {
            await _commandSender.SendAsync(new CreateApplicationCommand(name), cancellationToken);
        }

        [HttpDelete("{name:required}")]
        public async Task DeleteApplication(string name, CancellationToken cancellationToken)
        {
            await _commandSender.SendAsync(new DeleteApplicationCommand(name), cancellationToken);
        }
    }
}
