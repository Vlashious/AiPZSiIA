using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("account")]
public class AccountController : ControllerBase
{
    private HttpClient _client;

    public AccountController(HttpClient client)
    {
        _client = client;
    }
    [Route("google-login")]
    public IActionResult GoogleLogin()
    {
        var props = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        return Challenge(props, GoogleDefaults.AuthenticationScheme);
    }

    [Route("google-response")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        var claims = result.Principal.Claims;

        return new JsonResult(claims);
    }

    [Route("github-login")]
    public async Task<IActionResult> GithubLogin()
    {
        return Redirect($"https://github.com/login/oauth/authorize?client_id={Constants.GithubID}");
    }

    [Route("github-signin")]
    public async Task<IActionResult> GithubResponse()
    {
        var code = Request.Query["code"];

        var jsonContent = JsonContent.Create(new { code = code, client_id = Constants.GithubID, client_secret = Constants.GithubSecret });

        var response = await _client.PostAsync($"https://github.com/login/oauth/access_token?code={code}", jsonContent);
        var access_token = await response.Content.ReadAsStringAsync();

        var jwtToken = new JwtSecurityToken
        (
            Constants.Issuer,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: new SigningCredentials
            (
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Secret)),
                SecurityAlgorithms.HmacSha256
            )
        );

        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var jwtTokenWritten = jwtTokenHandler.WriteToken(jwtToken);

        return Redirect($"http://localhost:5002/account?access_token={jwtTokenWritten}");
    }
}