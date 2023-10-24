using System;
using System.Collections.Generic;

namespace TaxiVerificationIA.Models;

public partial class Color
{
    public int IdColor { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Taxis> Taxes { get; set; } = new List<Taxis>();
}
