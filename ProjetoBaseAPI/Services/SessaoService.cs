using Dapper;
using ProjetoBaseAPI.Data.Repositories;
using ProjetoBaseAPI.Models;

namespace ProjetoBaseAPI.Services
{
	public class SessaoService : ISessaoService
	{
		private readonly ISessaoRepository _sessaoRepository;

		public SessaoService(ISessaoRepository sessaoRepository)
		{
			_sessaoRepository = sessaoRepository;
		}

		public async Task<SessaoModel> CriarSessao(SessaoModel sessao)
		{
			// Aqui você pode adicionar lógica de negócio, como validações, 
			// antes de inserir a sessão no banco de dados
			return await _sessaoRepository.GerarSessao(sessao);
		}

		public async Task<SessaoModel> AtualizarToken(int codSessao, string tokenJWT, DateTime expTokenJWT)
		{
			return await _sessaoRepository.AtualizarToken(codSessao, tokenJWT, expTokenJWT);
		}

		public async Task<SessaoModel> AtualizarSignalR(int codSessao, int codSignalR)
		{
			return await _sessaoRepository.AtualizarSignalR(codSessao, codSignalR);
		}

		public async Task<SessaoModel> FinalizarSessao(int codSessao)
		{
			return await _sessaoRepository.FinalizarSessao(codSessao);
		}

		public async Task<SessaoModel> ManterAtiva(int codSessao)
		{
			return await _sessaoRepository.ManterAtiva(codSessao);
		}

		public async Task<SessaoModel> ObterPorId(int codSessao)
		{
			return await _sessaoRepository.ConsultarPorId(codSessao);
		}

		public async Task<IEnumerable<SessaoModel>> ObterTodas()
		{
			return await _sessaoRepository.ConsultarTodas();
		}

		public async Task DesativarSessoesExpiradas() // Corrigido: retorna Task
		{
			await _sessaoRepository.DesativarSessoesExpiradas();
		}
	}
}