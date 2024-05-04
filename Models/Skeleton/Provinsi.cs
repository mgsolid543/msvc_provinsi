using System;
using System.Collections.Generic;

namespace msvc_provinsi.Models.Skeleton;

public partial class Provinsi
{
    public int Id { get; set; }

    public string Nama { get; set; } = null!;

    public string Ibukota { get; set; } = null!;
}
