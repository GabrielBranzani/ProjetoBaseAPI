using ProjetoBaseAPI.Models;

namespace ProjetoBaseAPI.Data.Repositories
{
	public interface ISessaoRepository
	{
		Task<SessaoModel> GerarSessao(SessaoModel sessao);
		Task<SessaoModel> AtualizarToken(int codSessao, string tokenJWT, DateTime expTokenJWT);
		Task<SessaoModel> AtualizarSignalR(int codSessao, int codSignalR);
		Task<SessaoModel> FinalizarSessao(int codSessao);
		Task<SessaoModel> ManterAtiva(int codSessao);
		Task<SessaoModel> ConsultarPorId(int codSessao);
		Task<IEnumerable<SessaoModel>> ConsultarTodas();
		Task DesativarSessoesExpiradas();
	}
}