using ProjetoBaseAPI.Data;
using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Services;

namespace ProjetoBaseAPI.Configuration
{
	public static class DependencyInjectionConfig
	{
		public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
		{
			// Dependency Injection configuration
			services.AddScoped<DapperContext>();

			// Repositórios:
			services.AddScoped<UsuarioRepository>();

			// Serviços:
			services.AddScoped<TokenService>();

			// Background Service:


		}
	}
}