using System;
using System.Collections.Generic;

namespace QRGenerator.DataAccess.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string? Password { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Qrcodegenerate> Qrcodegenerates { get; set; } = new List<Qrcodegenerate>();

    public virtual ICollection<Qrreader> Qrreaders { get; set; } = new List<Qrreader>();
}
