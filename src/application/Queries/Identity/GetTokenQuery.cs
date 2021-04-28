using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Application.Common.Models;
using LibraryApplication.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Queries.Identity
{
    [Authorize(Roles = DefaultRoleNames.User)]
    public class GetTokenQuery : IRequest<GetTokenResponse>
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, GetTokenResponse>
    {
        private readonly IIdentityService _identityService;

        public GetTokenQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<GetTokenResponse> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = await _identityService.GetTokenAsync(
                request.Username,
                request.Password);

            return new GetTokenResponse()
            {
                Token = tokenHandler.WriteToken(token),
                Username = request.Username,
                Expires = token.ValidTo
            };
        }
    }
}
