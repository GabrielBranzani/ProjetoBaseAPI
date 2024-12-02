namespace ProjetoBaseAPI.Models.Gerenciador.RefreshTokens
{
	public class RefreshToken
	{
		public int codRefreshToken { get; set; }
		public int codUsuario { get; set; }
		public string token { get; set; }
		public DateTime datExpiracao { get; set; }
		public bool staAtivo { get; set; }
	}
}