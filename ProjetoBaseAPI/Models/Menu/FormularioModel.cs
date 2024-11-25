namespace ProjetoBaseAPI.Models.Menu
{
	public class FormularioModel
	{
		public int codFormulario { get; set; }
		public string nomFormulario { get; set; }
		public int codModulo { get; set; }
		public int codMenu { get; set; }
		public string nomRota { get; set; }
		public string nomIcone { get; set; }
		public int ordem { get; set; }
		public bool staAtivo { get; set; }
	}
}