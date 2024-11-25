namespace ProjetoBaseAPI.Models.Auth
{
	public class SessaoModel
	{
		public int codSessao { get; set; }
		public int codUsuario { get; set; }
		public string token { get; set; }
		public string refreshToken { get; set; }
		public DateTime expiracaoRefreshToken { get; set; }
		public DateTime datCriacao { get; set; }
		public bool staAtivo { get; set; }
	}
}