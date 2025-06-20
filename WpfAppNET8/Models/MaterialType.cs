using System;
using System.Collections.Generic;

namespace WpfAppNET8.Models;

public partial class MaterialType
{
    public string ТипМатериала { get; set; } = null!;

    public double ПроцентПотериСырья { get; set; }

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
}
