namespace ProjetoBaseAPI.Models
{
	public class UsuariosModel
	{
		public int codUsuario { get; set; }
		public string nomUsuario { get; set; }
		public string numCPF { get; set; }
		public string SenhaHash { get; set; }
		public DateTime datCriacao { get; set; }
		public bool staAtivo { get; set; }
	}
}
