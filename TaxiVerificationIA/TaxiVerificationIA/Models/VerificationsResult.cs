using System;
using System.Collections.Generic;

namespace TaxiVerificationIA.Models;

public partial class VerificationsResult
{
    public int IdVerificationResult { get; set; }

    public bool? IsPlate { get; set; }

    public decimal? PlateMatchAvg { get; set; }

    public bool? IsLabels { get; set; }

    public decimal? LabelsMatchAvg { get; set; }

    public bool? IsColor { get; set; }

    public decimal? ColorMatchAvg { get; set; }

    public bool? IsApproved { get; set; }

    public int? IdVerification { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? VerificationDate { get; set; }

    public virtual Verification? IdVerificationNavigation { get; set; }
}
