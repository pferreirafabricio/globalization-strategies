using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalizationApiSql.Domain;

public class Language
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The code is the ISO 639-1 code for the language.
    /// </summary>
    public string Code { get; set; } = string.Empty;
}
