using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalizationApiSql.Domain;

public class TechnicalMessage
{
    public int Id { get; set; }
    public int LanguageId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;

    public virtual Language Language { get; set; } = null!;
}
