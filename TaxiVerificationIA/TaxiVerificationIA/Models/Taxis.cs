using System;
using System.Collections.Generic;

namespace TaxiVerificationIA.Models;

public partial class Taxis
{
    public int IdTaxi { get; set; }

    public int? IdBrand { get; set; }

    public int? IdModel { get; set; }

    public int? Number { get; set; }

    public string? Plate { get; set; }

    public int? IdColor { get; set; }

    public virtual Brand? IdBrandNavigation { get; set; }

    public virtual Color? IdColorNavigation { get; set; }

    public virtual Model? IdModelNavigation { get; set; }

    public virtual ICollection<TaxiDriver> TaxiDrivers { get; set; } = new List<TaxiDriver>();

    public virtual ICollection<Verification> Verifications { get; set; } = new List<Verification>();
}
