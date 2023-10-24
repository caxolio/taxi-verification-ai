using System;
using System.Collections.Generic;

namespace TaxiVerificationIA.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public int? IdRol { get; set; }

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();

    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<TaxiDriver> TaxiDrivers { get; set; } = new List<TaxiDriver>();
}
