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
						uar.ResponseMessage = "User logged in";
						uar.UserResponseStatus = UserResponseStatus.Success;
					}
					else
					{
						uar.ResponseMessage = "Incorrect password";
						uar.UserResponseStatus = UserResponseStatus.Fail;
					}
				}
				else
				{
					uar.ResponseMessage = "User doesnt exist";
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
	}
}
