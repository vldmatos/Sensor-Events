using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[Route("account")]
	public class AccountController : ControllerBase
	{
		[HttpPost]
		[Route("login")]
		[AllowAnonymous]
		public ActionResult<dynamic> Authenticate([FromBody] User model)
		{
			if (model == null)
				return BadRequest(new { message = "Invalid parameter User!" });

			var user = SecurityService.GetUser(model.Username, model.Password);
			if (user == null)
				return NotFound(new { message = "Username or Password Invalid!" });


			var token = TokenService.GenerateToken(user);
			user.Password = string.Empty;

			return Ok(new { user, token });
		}
	}
}
