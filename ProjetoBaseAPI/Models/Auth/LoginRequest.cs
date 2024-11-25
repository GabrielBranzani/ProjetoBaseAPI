namespace ProjetoBaseAPI.Models.Auth
{
	public class LoginRequest
	{
		public string nomEmail { get; set; }
		public string senhaHash { get; set; }
	}
}