using ProjetoBaseAPI.BackgroundServices;
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

			// Repositórios
			services.AddScoped<IUsuarioRepository, UsuarioRepository>();
			services.AddScoped<ISessaoRepository, SessaoRepository>();

			// Serviços
			services.AddScoped<IUsuarioService, UsuarioService>();
			services.AddScoped<ISessaoService, SessaoService>();

			// Background Service
			services.AddHostedService<SessaoExpiradaBackgroundService>();

		}
	}
}