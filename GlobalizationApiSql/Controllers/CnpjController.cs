using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace GlobalizationApiSql.Controllers;

[ApiController]
[Route("api/cnpj")]
public class CnpjController(IStringLocalizer<CnpjController> localizer) : ControllerBase
{
    [HttpGet("fatal-error")]
    public IActionResult FatalError()
        => Ok(new
        {
            Message = new
            {
                Id = localizer["FAT001"].Name,
                Description = localizer["FAT001"].Value
            }

        });

    [HttpGet("error")]
    public IActionResult Error()
        => Ok(new
        {
            Message = new
            {
                Id = localizer["ERR001"].Name,
                Description = localizer["ERR001"].Value
            }
        });

    [HttpGet("warning")]
    public IActionResult Warning()
        => Ok(new
        {
            Message = new
            {
                Id = localizer["WARN001"].Name,
                Description = localizer["WARN001"].Value
            }
        });

    [HttpGet("info")]
    public IActionResult Info()
        => Ok(new
        {
            Message = new
            {
                Id = localizer["INFO001"].Name,
                Description = localizer["INFO001"].Value
            }
        });
}
