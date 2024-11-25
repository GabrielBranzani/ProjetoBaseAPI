namespace ProjetoBaseAPI.Models
{
	public class UsuarioModel
	{
		public int codUsuario { get; set; }
		public string nomUsuario { get; set; }
		public string nomEmail { get; set; }
		public string senhaHash { get; set; }
		public DateTime datCriacao { get; set; }
		public DateTime ultimoAcesso { get; set; }
		public bool staAtivo { get; set; }
	}
}