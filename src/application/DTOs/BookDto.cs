using AutoMapper;
using LibraryApplication.Application.Common.Interfaces;
using LibraryApplication.Application.Common.Mappings;
using LibraryApplication.Domain.Entities;
using System.Linq;

namespace LibraryApplication.Application.DTOs
{
    public class BookDto : IMapFrom<BookMeta>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public int Available { get; set; }

        public int Reserved { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookMeta, BookDto>()
                .ForMember(d => d.Reserved,
                s => s.MapFrom(
                    m => m.BooksInInventory
                    .Where(b => b.Reserved.Equals(true))
                    .ToList().Count))
                .ForMember(d => d.Available,
                s => s.MapFrom(
                    m => m.BooksInInventory
                    .Where(b => b.Reserved.Equals(false))
                    .ToList().Count));

        }
    }
}
