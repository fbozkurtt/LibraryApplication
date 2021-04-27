using LibraryApplication.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace LibraryApplication.Web.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId => Guid.Parse((_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)));

        public string Username => (_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name));
    }
}
