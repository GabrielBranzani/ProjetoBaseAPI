using Microsoft.IdentityModel.Tokens;
using ProjetoBaseAPI.Models.Gerenciador;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjetoBaseAPI.Services
{
	public class TokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GerarToken(UsuarioModel usuario)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
					{
										new Claim(ClaimTypes.NameIdentifier, usuario.codUsuario.ToString()),
										new Claim(ClaimTypes.Name, usuario.numCPF)
					}),
				Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:TokenExpirationMinutes"])), // Expira em 10 minutos
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public string GerarRefreshToken(UsuarioModel usuario)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]); // Pode usar a mesma chave ou uma diferente
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
					{
										new Claim(ClaimTypes.NameIdentifier, usuario.codUsuario.ToString()),
										new Claim(ClaimTypes.Name, usuario.numCPF)
					}),
				Expires = DateTime.UtcNow.AddMinutes(15), // Expira em 30 dias
				Issuer = _configuration["Jwt:Issuer"],
				Audience = _configuration["Jwt:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}