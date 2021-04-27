using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Application.Common.Mappings;
using LibraryApplication.Application.Common.Models;
using LibraryApplication.Application.DTOs;
using LibraryApplication.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Queries.Reservation
{
    [Authorize(Roles = DefaultRoleNames.User)]
    public class GetReservedBooksQuery : IRequest<PaginatedList<BookDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

    public class GetReservedBooksQueryHandler : IRequestHandler<GetReservedBooksQuery, PaginatedList<BookDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetReservedBooksQueryHandler(
            IApplicationDbContext dbContext,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BookDto>> Handle(GetReservedBooksQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.BookReservations
                .Where(b => b.UserId.Equals(_currentUserService.UserId))
                .Select(b => b.BookCopy)
                .OrderBy(b => b.Created)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize); ;
        }
    }
}
