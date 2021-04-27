using AutoMapper;
using LibraryApplication.Application.Common.Mappings;
using LibraryApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Application.DTOs
{
    public class ReservedBookDto : IMapFrom<BookCopy>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public bool Reserved { get; set; }

        public string QRCode { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookCopy, ReservedBookDto>()
                .ForMember(d => d.Title,
                s => s.MapFrom(
                    b => b.BookMeta.Title))
                .ForMember(d => d.Author,
                s => s.MapFrom(
                    b => b.BookMeta.Author))
                .ForMember(d => d.ISBN,
                s => s.MapFrom(
                    b => b.BookMeta.ISBN))
                .ForMember(d => d.Description,
                s => s.MapFrom(
                    b => b.BookMeta.Description))
                .ForMember(d => d.ShortDescription,
                s => s.MapFrom(
                    b => b.BookMeta.ShortDescription));
        }
    }
}
