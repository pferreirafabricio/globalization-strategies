using GlobalizationApiSql.Domain;
using GlobalizationApiSql.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlobalizationApiSql.Controllers;

[ApiController]
[Route("api/language")]
public class LanguageController(LanguageService languageService) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Language>> GetLanguagesAsync()
        => await languageService.GetLanguagesAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Language>> GetLanguageAsync(int id)
    {
        var language = await languageService.GetLanguageAsync(id);

        if (language is null)
            return NotFound();

        return language;
    }

    [HttpGet("code/{languageCode}")]
    public async Task<ActionResult<Language>> GetLanguageAsync(string languageCode)
    {
        var language = await languageService.GetLanguageAsync(languageCode);

        if (language is null)
            return NotFound();

        return language;
    }

    [HttpGet("code/{languageCode}/fallback")]
    public async Task<ActionResult<Language>> GetLanguageFallbackAsync(string languageCode)
    {
        var language = await languageService.GetLanguageFallbackAsync(languageCode);

        if (language is null)
            return NotFound();

        return language;
    }
}
