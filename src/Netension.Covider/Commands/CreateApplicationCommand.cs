using FluentValidation;
using Netension.Request;
using System;

namespace Netension.Covider.Commands
{
    public class CreateApplicationCommand : Command
    {
        public string Name { get; }

        public CreateApplicationCommand(string name, Guid? requestId = null) 
            : base(requestId ?? Guid.NewGuid())
        {
            Name = name;
        }
    }

    public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
    {
        public CreateApplicationCommandValidator()
        {
            RuleFor(c => c.Name)
                .Matches("^[a-z][a-z0-9_$()+/-]*$")
                .WithMessage("Invalid application name!");
            RuleFor(c => c.Name)
                .NotEmpty();
        }
    }
}
