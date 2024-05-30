using System.Globalization;
using GlobalizationApiSql.Domain;
using GlobalizationApiSql.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalizationApiSql.Controllers;

[ApiController]
[Route("api/technical-message")]
public class TechnicalMessageController(TechnicalMessageService technicalMessageService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<TechnicalMessage>> GetTechnicalMessagesAsync()
        => await technicalMessageService.GetTechnicalMessagesAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<TechnicalMessage>> GetTechnicalMessageAsync(int id)
    {
        var technicalMessage = await technicalMessageService.GetTechnicalMessageAsync(id);

        if (technicalMessage is null)
            return NotFound();

        return technicalMessage;
    }

    [HttpGet("code/{technicalCode}")]
    public async Task<ActionResult<TechnicalMessage>> GetTechnicalMessageAsync(string technicalCode)
    {
        var technicalMessage = await technicalMessageService.GetTechnicalMessageAsync(technicalCode);

        if (technicalMessage is null)
            return NotFound();

        return technicalMessage;
    }

    [HttpGet("code/{technicalCode}/fallback")]
    public async Task<ActionResult<TechnicalMessage>> GetTechnicalMessageFallbackAsync(string technicalCode)
    {
        var currentCultureCode = CultureInfo.CurrentCulture.Name;

        var technicalMessage = await technicalMessageService.GetTechnicalMessageFallbackAsync(technicalCode, currentCultureCode);

        if (technicalMessage is null)
            return NotFound();

        return technicalMessage;
    }
}
