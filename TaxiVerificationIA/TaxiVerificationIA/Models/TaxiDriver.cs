using System;
using System.Collections.Generic;

namespace TaxiVerificationIA.Models;

public partial class TaxiDriver
{
    public int IdTaxiDriver { get; set; }

    public string? Name { get; set; }

    public string? DriverLicense { get; set; }

    public int? IdTaxi { get; set; }

    public int? IdUser { get; set; }

    public virtual Taxis? IdTaxiNavigation { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<Verification> Verifications { get; set; } = new List<Verification>();
}
