using ProjetoBaseAPI.Models;

namespace ProjetoBaseAPI.Services
{
	public interface ISessaoService
	{
		Task<SessaoModel> CriarSessao(SessaoModel sessao);
		Task<SessaoModel> AtualizarToken(int codSessao, string tokenJWT, DateTime expTokenJWT);
		Task<SessaoModel> AtualizarSignalR(int codSessao, int codSignalR);
		Task<SessaoModel> FinalizarSessao(int codSessao);
		Task<SessaoModel> ManterAtiva(int codSessao);
		Task<SessaoModel> ObterPorId(int codSessao);
		Task<IEnumerable<SessaoModel>> ObterTodas();
		Task DesativarSessoesExpiradas();
	}
}