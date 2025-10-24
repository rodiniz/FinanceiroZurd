using System;
using System.Collections.Generic;

namespace myapi;

public partial class UsersSync
{
    public string RawJson { get; set; } = null!;

    public string Id { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
