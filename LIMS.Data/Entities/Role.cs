﻿using Microsoft.AspNetCore.Identity;

namespace LIMS.Data.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string? Description { get; set; }
    }
}
