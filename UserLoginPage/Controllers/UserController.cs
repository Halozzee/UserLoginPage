using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserLoginPage.Models;
using Newtonsoft.Json;

namespace UserLoginPage.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		public static Dictionary<string, string> loginToToken = new Dictionary<string, string>();

		[HttpPost("SignUp")]
		public string SignUp(string login, string passwordhash)
		{
			UserActionResponse uar = new UserActionResponse();

			try
			{
				if (!DataBaseLogic.UserExists(login))
				{
					if (DataBaseLogic.TryRegister(login, passwordhash))
					{
						uar.ResponseMessage = "User signed up";
						uar.UserResponseStatus = UserResponseStatus.Success;
					}
				}
				else
				{
					uar.ResponseMessage = "This user already exists";
					uar.UserResponseStatus = UserResponseStatus.Fail;
				}
			}
			catch (Exception)
			{
				uar.ResponseMessage = "Exception accured";
				uar.UserResponseStatus = UserResponseStatus.Exception;
			}

			return JsonConvert.SerializeObject(uar);
		}

		[HttpPost("LogIn")]
		public string LogIn(string login, string passwordhash)
		{
			UserActionResponse uar = new UserActionResponse();

			try
			{
				if (DataBaseLogic.UserExists(login))
				{
					if (DataBaseLogic.TryLogin(login, passwordhash))
					{
						if (loginToToken.ContainsKey(login))
							loginToToken.Remove(login);

						Random r = new Random();
						uar.ResponseMessage = "User logged in";
						uar.UserResponseStatus = UserResponseStatus.Success;
						uar.Token = r.Next(0, 129393993).ToString();

						loginToToken.Add(login, uar.Token);
					}
					else
					{
						uar.ResponseMessage = "Incorrect password or User doesnt exist";
						uar.UserResponseStatus = UserResponseStatus.Fail;
					}
				}
				else
				{
					uar.ResponseMessage = "Incorrect password or User doesnt exist";
					uar.UserResponseStatus = UserResponseStatus.Fail;
				}
			}
			catch (Exception)
			{
				uar.ResponseMessage = "Exception accured";
				uar.UserResponseStatus = UserResponseStatus.Exception;
			}

			return JsonConvert.SerializeObject(uar);
		}

		[HttpPost("ChangePassword")]
		public string ChangePassword(string login, string passwordhash)
		{
			UserActionResponse uar = new UserActionResponse();

			try
			{
				if (DataBaseLogic.UserExists(login))
				{
					if (DataBaseLogic.TryChangePassword(login, passwordhash))
					{
						uar.ResponseMessage = "User password changed";
						uar.UserResponseStatus = UserResponseStatus.Success;
					}
					else
					{
						uar.ResponseMessage = "Incorrect password or User doesnt exist";
						uar.UserResponseStatus = UserResponseStatus.Fail;
					}
				}
				else
				{
					uar.ResponseMessage = "Incorrect password or User doesnt exist";
					uar.UserResponseStatus = UserResponseStatus.Fail;
				}
			}
			catch (Exception)
			{
				uar.ResponseMessage = "Exception accured";
				uar.UserResponseStatus = UserResponseStatus.Exception;
			}

			return JsonConvert.SerializeObject(uar);
		}

		[HttpPost("LogOut")]
		public string LogOut(string login)
		{
			loginToToken.Remove(login);
			return JsonConvert.SerializeObject("true");
		}
	}
}
