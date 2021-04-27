using LibraryApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApplication.Infrastructure.Persistence.Configurations
{
    public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
    {
        public void Configure(EntityTypeBuilder<BookCopy> builder)
        {
            builder.Ignore(x => x.DomainEvents);

            builder.HasIndex(x => x.QRCode).IsUnique();

            builder.HasOne(bp => bp.BookMeta)
                   .WithMany(b => b.BooksInInventory)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
