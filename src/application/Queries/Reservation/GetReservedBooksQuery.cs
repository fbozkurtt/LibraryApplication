using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Application.Common.Mappings;
using LibraryApplication.Application.Common.Models;
using LibraryApplication.Application.DTOs;
using LibraryApplication.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Queries.Reservation
{
    [Authorize(Roles = DefaultRoleNames.User)]
    public class GetReservedBooksQuery : IRequest<PaginatedList<ReservedBookDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

    public class GetReservedBooksQueryHandler : IRequestHandler<GetReservedBooksQuery, PaginatedList<ReservedBookDto>>
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

        public async Task<PaginatedList<ReservedBookDto>> Handle(GetReservedBooksQuery request, CancellationToken cancellationToken)
        {
            var bookres = await _dbContext.BookReservations.ToListAsync();

            return await _dbContext.BookReservations
                .Where(b => b.UserId.Equals(_currentUserService.UserId))
                .Select(b => b.BookCopy)
                .OrderBy(b => b.Created)
                .AsNoTracking()
                .ProjectTo<ReservedBookDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
