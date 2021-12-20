namespace UserLoginPage.Models
{
	public class UserActionResponse
	{
		public UserResponseStatus UserResponseStatus { get; set; }
		public string ResponseMessage { get; set; }
		public string Token { get; set; }
	}

	public enum UserResponseStatus 
	{
		Success,
		Fail,
		Exception
	}
}
