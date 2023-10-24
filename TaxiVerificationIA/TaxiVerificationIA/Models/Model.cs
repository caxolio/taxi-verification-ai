using System;
using System.Collections.Generic;

namespace TaxiVerificationIA.Models;

public partial class Model
{
    public int IdModel { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual ICollection<Taxis> Taxes { get; set; } = new List<Taxis>();
}
