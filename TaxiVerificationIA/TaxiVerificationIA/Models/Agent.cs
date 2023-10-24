using System;
using System.Collections.Generic;

namespace TaxiVerificationIA.Models;

public partial class Agent
{
    public int IdAgent { get; set; }

    public string? Name { get; set; }

    public int? IdUser { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual User? IdUserNavigation { get; set; }

    public virtual ICollection<Verification> Verifications { get; set; } = new List<Verification>();
}
