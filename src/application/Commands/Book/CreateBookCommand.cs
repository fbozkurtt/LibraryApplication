using AutoMapper;
using LibraryApplication.Application.Common.Exceptions;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Application.Common.Mappings;
using LibraryApplication.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Book
{
    public class CreateBookCommand : IRequest, IMapFrom<Domain.Entities.Book>
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
            var book = _mapper.Map<Domain.Entities.Book>(request);

            await _dbContext.Books.AddAsync(book);

            book.DomainEvents.Add(new BookCreatedEvent(book));

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
