namespace ProjetoBaseAPI.Models.Menu
{
	public class MenuUsuarioModel
	{
		public int codGrupoUsuario { get; set; }
		public string nomGrupoUsuario { get; set; }
		public int codModulo { get; set; }
		public string nomModulo { get; set; }
		public string ModuloIcone { get; set; }
		public int OrdemModulo { get; set; }
		public int codMenu { get; set; }
		public string nomMenu { get; set; }
		public string MenuIcone { get; set; }
		public int OrdemMenu { get; set; }
		public int codFormulario { get; set; }
		public string nomFormulario { get; set; }
		public string FormularioRota { get; set; }
		public string FormularioIcone { get; set; }
		public int OrdemFormulario { get; set; }
		public int codPermissao { get; set; }
		public bool consultar { get; set; }
		public bool adicionar { get; set; }
		public bool editar { get; set; }
		public bool excluir { get; set; }
	}
}