using Microsoft.AspNetCore.SignalR;
using ProjetoBaseAPI.Services;

namespace ProjetoBaseAPI.Hubs
{
	public class UsuarioHub : Hub
	{
		private readonly ISessaoService _sessaoService;

		public UsuarioHub(ISessaoService sessaoService)
		{
			_sessaoService = sessaoService;
		}

		public async Task Conectar(int codSessao)
		{
			// Atualiza o código SignalR na sessão
			string connectionId = Context.ConnectionId;
			int codSignalR = int.Parse(connectionId.Substring(0, 8)); // Extrai os 8 primeiros caracteres e converte para int
			await _sessaoService.AtualizarSignalR(codSessao, codSignalR);
		}

		public async Task Desconectar(int codSessao)
		{
			// Finaliza a sessão
			await _sessaoService.FinalizarSessao(codSessao);
		}

		public async Task ManterAtiva(int codSessao)
		{
			// Mantém a sessão ativa
			await _sessaoService.ManterAtiva(codSessao);
		}
	}
}