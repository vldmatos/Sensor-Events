using API.Models;
using API.Security;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API.Services
{
	public class TokenService
	{
		public static string GenerateToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.Username),
					new Claim(ClaimTypes.Role, user.Role)
				}),

				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key.TokenPrivate), SecurityAlgorithms.HmacSha256Signature),

				Expires = DateTime.UtcNow.AddHours(1)
			};

			var securityToken = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(securityToken);
		}
	}
}
