﻿using LibraryApplication.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApplication.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual List<BookReservation> BookReservations { get; set; }
    }
}
