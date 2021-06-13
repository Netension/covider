using Microsoft.Extensions.Logging;
using Netension.Core.Exceptions;
using Netension.Covider.Application.Clients;
using Netension.Covider.Application.Extensions;
using Netension.Request.Abstraction.Senders;
using Netension.Request.Handlers;
using System.Threading;
using System.Threading.Tasks;

namespace Netension.Covider.Application.CommandHandlers
{
    public class DeleteApplicationCommandHandler : CommandHandler<DeleteApplicationCommand>
    {
        private readonly IApplicationRepository _storage;

        public DeleteApplicationCommandHandler(IApplicationRepository storage, IQuerySender querySender, ILogger<DeleteApplicationCommand> logger) 
            : base(querySender, logger)
        {
            _storage = storage;
        }

        public override async Task HandleAsync(DeleteApplicationCommand command, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Delete database of {name} application", command.Name);
            if (!await _storage.IsApplicationExistsAsync(command.Name, cancellationToken))
            {
                Logger.LogDebug("{name} application does not exist", command.Name);
                throw new VerificationException($"{command.Name} application does not exist");
            }

            await _storage.DeleteAsync(command.Name, cancellationToken);
        }
    }
}
