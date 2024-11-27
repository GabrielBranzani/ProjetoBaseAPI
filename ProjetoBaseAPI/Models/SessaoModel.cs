namespace ProjetoBaseAPI.Models
{
	public class SessaoModel
	{
		public int codSessao { get; set; }
		public int codUsuario { get; set; }
		public string TokenJWT { get; set; }
		public DateTime expTokenJWT { get; set; }
		public int codSignalR { get; set; }
		public DateTime datInicioAcesso { get; set; }
		public DateTime datUltimoAcesso { get; set; }
		public bool staAtivo { get; set; }
	}
}
