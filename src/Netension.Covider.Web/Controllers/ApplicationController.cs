using Microsoft.AspNetCore.Mvc;
using Netension.Covider.Commands;
using Netension.Request.Abstraction.Senders;
using System.Threading;
using System.Threading.Tasks;

namespace Netension.Covider.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ICommandSender _commandSender;

        public ApplicationController(ICommandSender commandSender)
        {
            _commandSender = commandSender;
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
