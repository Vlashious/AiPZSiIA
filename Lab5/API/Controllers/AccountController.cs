using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
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
        return Redirect($@"https://accounts.google.com/o/oauth2/v2/auth?client_id={Constants.GoogleID}&redirect_uri=http://localhost:5000/account/google-signin&response_type=code&scope=https://www.googleapis.com/auth/userinfo.profile");
    }

    [Route("google-signin")]
    public async Task<IActionResult> GoogleResponse()
    {
        var code = Request.Query["code"];

        var jsonContent = JsonContent.Create(new {client_id = Constants.GoogleID, client_secret = Constants.GoogleSecret, code = code });

        var response = await _client.PostAsync($"https://oauth2.googleapis.com/token", jsonContent);
        var access_token = await response.Content.ReadAsStringAsync();

        return Redirect($"http://localhost:5002/account?access_token={IssueToken()}");
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

        return Redirect($"http://localhost:5002/account?access_token={IssueToken()}");
    }

    private string IssueToken()
    {

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

        return jwtTokenWritten;
    }
}