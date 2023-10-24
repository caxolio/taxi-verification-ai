using System;
using System.Collections.Generic;

namespace TaxiVerificationIA.Models;

public partial class Verification
{
    public int IdVerification { get; set; }

    public string? Folio { get; set; }

    public int? IdTaxi { get; set; }

    public int? IdTaxiDriver { get; set; }

    public int? IdAgent { get; set; }

    public DateTime? CreateDate { get; set; }

    public virtual Agent? IdAgentNavigation { get; set; }

    public virtual TaxiDriver? IdTaxiDriverNavigation { get; set; }

    public virtual Taxis? IdTaxiNavigation { get; set; }

    public virtual ICollection<VerificationsImage> VerificationsImages { get; set; } = new List<VerificationsImage>();

    public virtual ICollection<VerificationsResult> VerificationsResults { get; set; } = new List<VerificationsResult>();
}
