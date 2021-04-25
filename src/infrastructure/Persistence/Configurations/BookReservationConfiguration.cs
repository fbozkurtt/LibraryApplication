using LibraryApplication.Domain.Entities;
using LibraryApplication.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Infrastructure.Persistence.Configurations
{
    class BookReservationConfiguration : IEntityTypeConfiguration<BookReservation>
    {
        public void Configure(EntityTypeBuilder<BookReservation> builder)
        {
            builder.HasOne(br => br.User)
                   .WithMany(u => (u as ApplicationUser).BookReservations)
                   .HasForeignKey(br => br.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(br => br.Book)
                   .WithOne(b => b.BookReservation)
                   .HasForeignKey<BookProduct>(b => b.BookReservationId)
                   .HasForeignKey<BookReservation>(b => b.BookProductId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
