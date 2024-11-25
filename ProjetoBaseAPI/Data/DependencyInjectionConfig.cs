using ProjetoBaseAPI.Data;
using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Services;
//using ProjetoBaseAPI.Data.Repositories;
//using ProjetoBaseAPI.Services;

namespace ProjetoBaseAPI.Configuration
{
	public static class DependencyInjectionConfig
	{
		public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
		{
			// Dependency Injection configuration
			services.AddScoped<DapperContext>();

			// Repositórios
			services.AddScoped<IAuthRepository, AuthRepository>();
			services.AddScoped<IGrupoRepository, GrupoRepository>();
			services.AddScoped<IModuloRepository, ModuloRepository>();
			services.AddScoped<IMenuRepository, MenuRepository>();
			services.AddScoped<IFormularioRepository, FormularioRepository>();
			services.AddScoped<IPermissaoRepository, PermissaoRepository>();
			services.AddScoped<IMenuUsuarioRepository, MenuUsuarioRepository>(); // Novo repositório

			// Serviços
			services.AddScoped<AuthService>();
			services.AddScoped<GrupoService>();
			services.AddScoped<ModuloService>();
			services.AddScoped<MenuService>();
			services.AddScoped<FormularioService>();
			services.AddScoped<PermissaoService>();
			services.AddScoped<MenuUsuarioService>();
		}
	}
}