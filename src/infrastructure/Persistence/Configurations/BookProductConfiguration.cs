using LibraryApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Infrastructure.Persistence.Configurations
{
    public class BookProductConfiguration : IEntityTypeConfiguration<BookProduct>
    {
        public void Configure(EntityTypeBuilder<BookProduct> builder)
        {
            builder.HasOne(bp => bp.Book)
                   .WithMany(b => b.BooksInInventory)
                   .HasForeignKey(bp => bp.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
