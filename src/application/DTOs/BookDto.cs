using AutoMapper;
using LibraryApplication.Application.Common.Mappings;
using LibraryApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Application.DTOs
{
    public class BookDto : IMapFrom<Book>
    {
        public string Name { get; set; }

        public string Author { get; set; }

        public long ISBN { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Book, BookDto>()
                .ForMember(d => d.Quantity, s => s.MapFrom(s => s.BooksInInventory.Count));
        }
    }
}
