using LibraryApplication.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Infrastructure.Persistence.Configurations
{
    public class BookMetaConfiguration : IEntityTypeConfiguration<BookMeta>
    {
        public void Configure(EntityTypeBuilder<BookMeta> builder)
        {
            builder.Ignore(x => x.DomainEvents);

            builder.HasIndex(x => x.ISBN).IsUnique();

            builder.HasData(new List<BookMeta>()
            {
                new BookMeta()
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    ISBN = "9781593275846",
                    Title = "Eloquent JavaScript, Second Edition",
                    Author = "Marijn Haverbeke",
                    ShortDescription = "JavaScript lies at the heart of almost every modern web application, from social apps to the newest browser-based games. Though simple for beginners to pick up and play with, JavaScript is a flexible, complex language that you can use to build full-scale applications.",
                },
                new BookMeta()
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    ISBN = "9781449331818",
                    Title = "Learning JavaScript Design Patterns",
                    Author = "Addy Osmani",
                    ShortDescription= "With Learning JavaScript Design Patterns, you'll learn how to write beautiful, structured, and maintainable JavaScript by applying classical and modern design patterns to the language. If you want to keep your code efficient, more manageable, and up-to-date with the latest best practices, this book is for you.",
                },
                new BookMeta()
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    ISBN= "9781449365035",
                    Title= "Speaking JavaScript",
                    Author= "Axel Rauschmayer",
                    ShortDescription = "Like it or not, JavaScript is everywhere these days-from browser to server to mobile-and now you, too, need to learn the language or dive deeper than you have. This concise book guides you into and through JavaScript, written by a veteran programmer who once found himself in the same position.",
                },
                new BookMeta()
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    ISBN = "9781491950296",
                    Title = "Programming JavaScript Applications",
                    Author = "Eric Elliott",
                    ShortDescription = "Take advantage of JavaScript's power to build robust web-scale or enterprise applications that are easy to extend and maintain. By applying the design patterns outlined in this practical book, experienced JavaScript developers will learn how to write flexible and resilient code that's easier-yes, easier-to work with as your code base grows.",
                },
                new BookMeta()
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    ISBN = "9781593277574",
                    Title = "Understanding ECMAScript 6",
                    Author = "Nicholas C. Zakas",
                    ShortDescription= "ECMAScript 6 represents the biggest update to the core of JavaScript in the history of the language. In Understanding ECMAScript 6, expert developer Nicholas C. Zakas provides a complete guide to the object types, syntax, and other exciting changes that ECMAScript 6 brings to JavaScript.",
                },
                new BookMeta()
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    ISBN = "9781491904244",
                    Title = "You Don't Know JS",
                    Author = "Kyle Simpson",
                    ShortDescription = "No matter how much experience you have with JavaScript, odds are you don’t fully understand the language. As part of the \"You Don’t Know JS\" series, this compact guide focuses on new features available in ECMAScript 6 (ES6), the latest version of the standard upon which JavaScript is built.",
                },
                new BookMeta()
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    ISBN = "9781449325862",
                    Title = "Git Pocket Guide",
                    Author = "Richard E. Silverman",
                    ShortDescription = "This pocket guide is the perfect on-the-job companion to Git, the distributed version control system. It provides a compact, readable introduction to Git for new users, as well as a reference to common commands and procedures for those of you with Git experience.",
                },
                new BookMeta()
                {
                    Created = DateTime.Now,
                    Id = Guid.NewGuid(),
                    ISBN = "9781449337711",
                    Title = "Designing Evolvable Web APIs with ASP.NET",
                    Author = "Glenn Block, et al.",
                    ShortDescription = "Design and build Web APIs for a broad range of clients—including browsers and mobile devices—that can adapt to change over time. This practical, hands-on guide takes you through the theory and tools you need to build evolvable HTTP services with Microsoft’s ASP.NET Web API framework. In the process, you’ll learn how design and implement a real-world Web API.",
                }
            });
        }
    }
}
