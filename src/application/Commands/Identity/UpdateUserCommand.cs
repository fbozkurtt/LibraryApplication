using LibraryApplication.Constants;
using Microsoft.AspNetCore.Authorization;

namespace LibraryApplication.Application.Commands.Identity
{
    [Authorize(Roles = DefaultRoleNames.User)]
    class UpdateUserCommand
    {
    }
}
