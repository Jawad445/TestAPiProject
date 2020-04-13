﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIDev.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string GivenName { get; set; }
    }
}
