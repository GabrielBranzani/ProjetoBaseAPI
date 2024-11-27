using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjetoBaseAPI.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProjetoBaseAPI.BackgroundServices
{
	public class SessaoExpiradaBackgroundService : BackgroundService
	{
		private readonly ILogger<SessaoExpiradaBackgroundService> _logger;
		private readonly IServiceScopeFactory _scopeFactory;
		private readonly TimeSpan _intervaloVerificacao;

		public SessaoExpiradaBackgroundService(ILogger<SessaoExpiradaBackgroundService> logger,
																					 IServiceScopeFactory scopeFactory,
																					 IConfiguration configuration)
		{
			_logger = logger;
			_scopeFactory = scopeFactory;
			_intervaloVerificacao = TimeSpan.FromMinutes(Convert.ToDouble(configuration["Jwt:AccessTokenExpirationMinutes"]));
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				try
				{
					_logger.LogInformation("Verificando e desativando sessões expiradas...");

					using (var scope = _scopeFactory.CreateScope())
					{
						var sessaoService = scope.ServiceProvider.GetRequiredService<ISessaoService>();
						await sessaoService.DesativarSessoesExpiradas();
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Erro ao verificar e desativar sessões expiradas.");
				}

				await Task.Delay(_intervaloVerificacao, stoppingToken);
			}
		}
	}
}