using System;
using System.Collections.Generic;

namespace CoreWebAPI.Models;

public partial class WcfEnter
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public string? Gender { get; set; }

    public string? Country { get; set; }
}
