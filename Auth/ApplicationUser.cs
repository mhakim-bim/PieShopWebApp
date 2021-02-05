﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace PieShop.Auth
{
    public class ApplicationUser : IdentityUser
    {

        public DateTime? BirthDate { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

    }
}
