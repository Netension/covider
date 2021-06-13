using FluentValidation;
using Netension.Request;
using System;

namespace Netension.Covider
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
            RuleFor(c => c.Name)
                .NotEmpty();
        }
    }
}
