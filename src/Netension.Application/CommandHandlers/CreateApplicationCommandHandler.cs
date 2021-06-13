using Microsoft.Extensions.Logging;
using Netension.Core.Exceptions;
using Netension.Covider.Application.Clients;
using Netension.Covider.Application.Extensions;
using Netension.Covider.Commands;
using Netension.Request.Abstraction.Senders;
using Netension.Request.Handlers;
using System.Threading;
using System.Threading.Tasks;

namespace Netension.Covider.Application.CommandHandlers
{
    public class CreateApplicationCommandHandler : CommandHandler<CreateApplicationCommand>
    {
        private readonly IStorage _storage;

        public CreateApplicationCommandHandler(IStorage storage, IQuerySender querySender, ILogger<CreateApplicationCommandHandler> logger) 
            : base(querySender, logger)
        {
            _storage = storage;
        }

        public override async Task HandleAsync(CreateApplicationCommand command, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Create database for {name} application", command.Name);
            if (await _storage.IsApplicationExistsAsync(command.Name, cancellationToken))
            {
                Logger.LogDebug("{name} application has been already created", command.Name);
                throw new VerificationException($"{command.Name} application has been already created");
            }

            await _storage.CreateApplicationAsync(command.Name, cancellationToken);
        }
    }
}
