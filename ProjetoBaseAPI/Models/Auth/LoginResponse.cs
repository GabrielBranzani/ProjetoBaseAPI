namespace ProjetoBaseAPI.Models.Auth
{
	public class LoginResponse
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public DateTime? AccessTokenExpiration { get; set; }
	}
}