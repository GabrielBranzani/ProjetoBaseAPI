using Microsoft.IdentityModel.Tokens;
using ProjetoBaseAPI.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoBaseAPI.Utils
{
	public static class TokenHelper
	{
		public static LoginResponse GenerateAccessToken(
			string username,
			IEnumerable<Claim> claims,
			IConfiguration configuration)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:AccessTokenExpirationMinutes"])),
				Issuer = configuration["Jwt:Issuer"],
				Audience = configuration["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return new LoginResponse
			{
				AccessToken = tokenHandler.WriteToken(token),
				RefreshToken = GenerateRefreshToken(configuration),
				AccessTokenExpiration = tokenDescriptor.Expires
			};
		}

		public static string GenerateRefreshToken(IConfiguration configuration)
		{
			var randomNumber = new byte[32];
			using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
			rng.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		public static ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration configuration)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Key"])),
				ValidateLifetime = false
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
			if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
				throw new SecurityTokenException("Invalid token");

			return principal;
		}
	}
}