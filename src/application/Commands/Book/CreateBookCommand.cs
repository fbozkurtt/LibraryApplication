using AutoMapper;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Application.Common.Mappings;
using LibraryApplication.Constants;
using LibraryApplication.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Book
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class CreateBookCommand : IRequest, IMapFrom<Domain.Entities.BookMeta>
    {
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Author { get; set; }

        public long ISBN { get; set; }

        [MaxLength(4000)]
        public string Description { get; set; }

        [MaxLength(1000)]
        public string ShortDescription { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<Domain.Entities.BookMeta>(request);

            await _dbContext.BookMetas.AddAsync(book);

            book.DomainEvents.Add(new BookCreatedEvent(book));

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
