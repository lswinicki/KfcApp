﻿using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<int>
{
    public required string FirstName { get; set; } 
    public required string LastName { get; set; }
}
