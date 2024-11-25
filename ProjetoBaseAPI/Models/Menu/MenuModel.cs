namespace ProjetoBaseAPI.Models.Menu
{
	public class MenuModel
	{
		public int codMenu { get; set; }
		public string nomMenu { get; set; }
		public int codModulo { get; set; }
		public string nomIcone { get; set; }
		public int ordem { get; set; }
		public bool staAtivo { get; set; }
	}
}