using ProjetoBaseAPI.Models;
using ProjetoBaseAPI.Models.Auth;

namespace ProjetoBaseAPI.Data.Repositories
{
	public interface IAuthRepository
	{
		Task<UsuarioModel> ObterUsuarioPorEmail(string nomEmail);
		Task<SessaoModel> CriarSessao(SessaoModel sessao);
		Task<SessaoModel> ObterSessaoPorRefreshToken(string refreshToken);
		Task<SessaoModel> AtualizarSessao(SessaoModel sessao);
		Task RemoverSessao(string refreshToken);
	}
}