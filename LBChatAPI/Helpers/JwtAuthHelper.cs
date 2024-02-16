using LBChatAPI.Models;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace SignalRServer.Helpers
{
	public class JwtAuthHelper
	{
        private readonly IConfiguration _configuration;

        public JwtAuthHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateUserToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();

            var appSettings = _configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            var secretKey = appSettings?.SecretKey ?? "";
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
					new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
				Expires = DateTime.UtcNow.AddHours(10),

				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
    }
}

