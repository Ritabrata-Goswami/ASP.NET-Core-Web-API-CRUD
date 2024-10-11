using System;
using System.Collections.Generic;

namespace CoreWebAPI.Models;

public partial class Student
{
    public int Id { get; set; }

    public string? StudentName { get; set; }

    public string? Class { get; set; }

    public string? DoB { get; set; }
}
