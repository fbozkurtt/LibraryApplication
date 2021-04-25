using AutoMapper;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Application.Common.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Book
{
    public class UpdateBookCommand : IRequest
    {
        public long ISBN { get; set; }

        public string NewName { get; set; }

        public string NewAuthor { get; internal set; }

        public string NewDescription { get; set; }

        public string NewShortDescription { get; set; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Unit>
    {
        private readonly IApplicationDbContext _dbContext;

        public UpdateBookCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbContext.Books.SingleOrDefaultAsync(b => b.ISBN.Equals(request.ISBN));

            book.Name = request.NewName;
            book.Author = request.NewAuthor;
            book.Description = request.NewDescription;
            book.ShortDescription = request.NewShortDescription;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
