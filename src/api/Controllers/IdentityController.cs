using LibraryApplication.Application.Commands.Identity;
using LibraryApplication.Application.Common.Models;
using LibraryApplication.Application.Queries.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Web.API.Controllers
{
    public class IdentityController : ApiControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<GetTokenResponse> GetToken(GetTokenQuery query)
               => await Mediator.Send(query);

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
