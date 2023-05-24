using System;
using System.Collections.Generic;

namespace CarPoolingWebAPI.Models;

public partial class LoginDetail
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual UserDetail? UserDetail { get; set; }
}
