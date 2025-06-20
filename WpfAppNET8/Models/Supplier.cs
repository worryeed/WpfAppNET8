using System;
using System.Collections.Generic;

namespace WpfAppNET8.Models;

public partial class Supplier
{
    public string НаименованиеПоставщика { get; set; } = null!;

    public string ТипПоставщика { get; set; } = null!;

    public long Инн { get; set; }

    public int Рейтинг { get; set; }

    public DateTime ДатаНачалаРаботыСПоставщиком { get; set; }

    public virtual ICollection<Material> НаименованиеМатериалаs { get; set; } = new List<Material>();
}
