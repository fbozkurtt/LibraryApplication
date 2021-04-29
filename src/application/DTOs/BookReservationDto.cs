using AutoMapper;
using LibraryApplication.Application.Common.Mappings;
using LibraryApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryApplication.Application.DTOs
{
    public class BookReservationDto : IMapFrom<BookReservation>
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public string Description { get; set; }

        public string ShortDescription { get; set; }

        public string QRCode { get; set; }

        public bool Returned { get; set; }

        public DateTime DateReserved { get; set; }

        public DateTime? DateReturned { get; set; }

        public DateTime? ReservationEnds { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BookReservation, BookReservationDto>()
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
                    b => b.BookMeta.ShortDescription))
                .ForMember(d => d.DateReserved,
                s => s.MapFrom(
                    b => b.Created))
                .ForMember(d => d.DateReturned,
                s => s.MapFrom(
                    b => b.LastModified.Value))
                .ForMember(d => d.QRCode,
                s => s.MapFrom(
                    b => b.BookCopy.QRCode));
        }
    }
}
