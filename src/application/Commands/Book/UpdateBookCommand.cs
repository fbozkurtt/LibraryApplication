using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryApplication.Application.Commands.Book
{
    [Authorize(Roles = DefaultRoleNames.Admin)]
    public class UpdateBookCommand : IRequest
    {
        public long ISBN { get; }

        public string NewName { get; set; }

        public string NewAuthor { get; set; }

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
            var book = await _dbContext.BookMetas.SingleOrDefaultAsync(b => b.ISBN.Equals(request.ISBN));

            book.Title = request.NewName;
            book.Author = request.NewAuthor;
            book.Description = request.NewDescription;
            book.ShortDescription = request.NewShortDescription;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
