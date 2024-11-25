namespace ProjetoBaseAPI.Models.Permissoes
{
	public class PermissaoModel
	{
		public int codPermissao { get; set; }
		public int codGrupoUsuario { get; set; }
		public int codFormulario { get; set; }
		public bool consultar { get; set; }
		public bool adicionar { get; set; }
		public bool editar { get; set; }
		public bool excluir { get; set; }
		public bool staAtivo { get; set; }
	}
}