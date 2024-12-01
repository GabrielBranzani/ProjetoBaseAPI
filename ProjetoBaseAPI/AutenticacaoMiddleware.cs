//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using ProjetoBaseAPI.Services;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ProjetoBaseAPI
//{
//	public class AutenticacaoMiddleware
//	{
//		private readonly RequestDelegate _next;
//		private readonly IConfiguration _configuration;

//		public AutenticacaoMiddleware(RequestDelegate next, IConfiguration configuration)
//		{
//			_next = next;
//			_configuration = configuration;
//		}

//		public async Task InvokeAsync(HttpContext context)
//		{
//			var tokenHandler = new JwtSecurityTokenHandler();
//			var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

//			string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

//			if (token != null)
//			{
//				try
//				{
//					tokenHandler.ValidateToken(token, new TokenValidationParameters
//					{
//						ValidateIssuerSigningKey = true,
//						IssuerSigningKey = new SymmetricSecurityKey(key),
//						ValidateIssuer = false,
//						ValidateAudience = false,
//						ClockSkew = TimeSpan.Zero
//					}, out SecurityToken validatedToken);

//					var jwtToken = (JwtSecurityToken)validatedToken;
//					var codSessao = int.Parse(jwtToken.Claims.First(x => x.Type == "codSessao").Value);

//					// Resolver o ISessaoService usando context.RequestServices
//					var sessaoService = context.RequestServices.GetRequiredService<ISessaoService>();

//					// Verificar se a sessão está ativa
//					var sessao = await sessaoService.ObterPorId(codSessao);
//					if (sessao == null || !sessao.staAtivo)
//					{
//						context.Response.StatusCode = 401; // Unauthorized
//						return;
//					}
//				}
//				catch (SecurityTokenException)
//				{
//					// Token inválido
//					context.Response.StatusCode = 401; // Unauthorized
//					return;
//				}
//			}

//			// Continue o pipeline de requisições
//			await _next(context);
//		}
//	}
//}