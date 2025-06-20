using System;
using System.Collections.Generic;

namespace WpfAppNET8.Models;

public partial class Material
{
    public string НаименованиеМатериала { get; set; } = null!;

    public string ТипМатериала { get; set; } = null!;

    public decimal ЦенаЕдиницыМатериала { get; set; }

    public int КоличествоНаСкладе { get; set; }

    public int МинимальноеКоличество { get; set; }

    public int КоличествоВУпаковке { get; set; }

    public string ЕдиницаИзмерения { get; set; } = null!;

    public virtual MaterialType ТипМатериалаNavigation { get; set; } = null!;

    public virtual ICollection<Supplier> Поставщикs { get; set; } = new List<Supplier>();
}
