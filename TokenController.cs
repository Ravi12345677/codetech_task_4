using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public TokenController(IConfiguration config)
    {
        _config = config;
        _httpClient = new HttpClient();
    }

    [HttpGet]
    public async Task<IActionResult> GetToken()
    {
        var secret = _config["Bot:DirectLineSecret"];
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", secret);

        var res = await _httpClient.PostAsync("https://directline.botframework.com/v3/directline/tokens/generate", null);
        var content = await res.Content.ReadAsStringAsync();
        return Content(content, "application/json");
    }
}
