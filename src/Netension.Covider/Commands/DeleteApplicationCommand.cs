using FluentValidation;
using Netension.Request;
using System;

namespace Netension.Covider.Commands
{
    public class DeleteApplicationCommand : Command
    {
        public string Name { get; }

        public DeleteApplicationCommand(string name)
            : base(Guid.NewGuid())
        {
            Name = name;
        }
    }

    public class DeleteApplicationCommandValidator : AbstractValidator<DeleteApplicationCommand>
    {
        public DeleteApplicationCommandValidator()
        {
            
        }
    }
}
