using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Application.Common.Mappings;
using LibraryApplication.Application.Common.Models;
using LibraryApplication.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Queries.Book
{
    public class GetBooksQuery : IRequest<PaginatedList<BookDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, PaginatedList<BookDto>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Books
                .OrderBy(b => b.Created)
                .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize); ;
        }
    }
}
