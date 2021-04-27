using AutoMapper;
using LibraryApplication.Application.Common.Mappings;
using LibraryApplication.Domain.Entities;

namespace LibraryApplication.Application.DTOs
{
    public class BookDto : IMapFrom<BookMeta>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public long ISBN { get; set; }

        public string QRCode { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookMeta, BookDto>()
                .ForMember(d => d.Quantity, s => s.MapFrom(s => s.BooksInInventory.Count));
        }
    }
}
