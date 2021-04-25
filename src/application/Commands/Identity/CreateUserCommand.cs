using LibraryApplication.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Identity
{
    public class CreateUserCommand : IRequest
    {
        [Required]
        [MaxLength(64)]
        [MinLength(5)]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IIdentityService _IdentityService;

        public CreateUserCommandHandler(IIdentityService IdentityService)
        {
            _IdentityService = IdentityService;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _IdentityService.CreateUserAsync(
                request.Username,
                request.Password);

            return Unit.Value;
        }
    }
}
