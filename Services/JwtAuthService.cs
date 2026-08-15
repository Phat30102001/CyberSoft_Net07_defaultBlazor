using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace buoi18.Services;

public class JwtAuthService
{
    private readonly IConfiguration _configuration;
    private string _jwtKey = "";
    private string _jwtIssuer = "";
    private string _jwtAudience = "";
    private int _jwtExpireMinutes = 10;

    public JwtAuthService(IConfiguration configuration)
    {
        _configuration = configuration;
        _jwtKey = _configuration["Jwt:Key"] ?? "";
        _jwtIssuer = _configuration["Jwt:Issuer"] ?? "";
        _jwtAudience = _configuration["Jwt:Audience"] ?? "";
        _jwtExpireMinutes = int.Parse(_configuration["Jwt:ExpireMinutes"] ?? "10");
    }


    public string GenerateToken(string username, string role)
    {
        // thông tin về user cần dc đóng gói vào phần thân của jwt
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role),
            new("LopHoc","NET07") // thêm thông tin riêng
        };
        var sKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var credentials = new SigningCredentials(sKey, SecurityAlgorithms.HmacSha256);

        // 
        var token = new JwtSecurityToken(
           issuer: _jwtIssuer,
           audience: _jwtAudience,
           claims: claims,
           notBefore: DateTime.UtcNow,
           expires: DateTime.UtcNow.AddMinutes(_jwtExpireMinutes),
           signingCredentials: credentials
        );

        var strToken = new JwtSecurityTokenHandler().WriteToken(token);
        return strToken.ToString();
    }

    public List<Claim> CheckToken(string token)
    {
        // ktra sơ qua về thông tin . chưa check xem token này có phải mình ký hay không
        // var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
        // var tokenClaims = jwt.Claims.ToList();
        // return tokenClaims;
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = _jwtIssuer,
            ValidateAudience = true,
            ValidAudience = _jwtAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        return principal.Claims.ToList();

    }
    public ClaimsPrincipal CheckTokenState(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = _jwtIssuer,
            ValidateAudience = true,
            ValidAudience = _jwtAudience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        return principal;

    }
}
// ai tạo token 
// token dùng để làm gì
// secret key : 