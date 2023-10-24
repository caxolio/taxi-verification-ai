using System;
using System.Collections.Generic;

namespace TaxiVerificationIA.Models;

public partial class VerificationsImage
{
    public int IdVerificationImages { get; set; }

    public byte[]? FrontalImage { get; set; }

    public byte[]? LeftSideImage { get; set; }

    public byte[]? RightSideImage { get; set; }

    public int? IdVerification { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? FrontImageName { get; set; }

    public string? LeftImageName { get; set; }

    public string? RightImageName { get; set; }

    public string? FrontImageUrl { get; set; }

    public string? LeftImageUrl { get; set; }

    public string? RightImageUrl { get; set; }

    public bool? AcceptCompliance { get; set; }

    public virtual Verification? IdVerificationNavigation { get; set; }
}
