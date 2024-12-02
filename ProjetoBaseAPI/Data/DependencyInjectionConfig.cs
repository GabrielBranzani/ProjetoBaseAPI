using ProjetoBaseAPI.Data;

namespace ProjetoBaseAPI.Configuration
{
	public static class DependencyInjectionConfig
	{
		public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
		{
			// Dependency Injection configuration
			services.AddScoped<DapperContext>();

			// Repositórios:


			// Serviços:
			

			// Background Service:
			

		}
	}
}