using System;
using System.Collections.Generic;

namespace QRGenerator.DataAccess.Models;

public partial class Qrcodegenerate
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public DateTime? Created { get; set; }

    public string? Text { get; set; }

    public virtual User? UsernameNavigation { get; set; }
}
