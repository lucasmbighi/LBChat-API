using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LBChatAPI.Models.Auth;
using SignalRServer.Helpers;
using Microsoft.EntityFrameworkCore;
using LBChatAPI.Models;

namespace SignalRServer.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserContext _userContext;
        private readonly ChatContext _chatContext;
        private readonly MessageContext _messageContext;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserContext userContext,
            ChatContext chatContext,
            MessageContext messageContext,
            IConfiguration configuration
            )
        {
            _userContext = userContext;
            _chatContext = chatContext;
            _messageContext = messageContext;
            _configuration = configuration;

            UserRepository.AddTestUsers(_userContext);
            ChatRepository.AddTestChats(_chatContext);
            MessageRepository.AddTestMessages(_messageContext);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/v1/users/auth")]
        public async Task<IActionResult> Auth([FromBody] AuthRequest request)
        {
            try
            {
                var userExists = await _userContext.Users
                    .Where(x => x.Email == request.Email && x.Password == request.Password)
                    .FirstOrDefaultAsync();

                if (userExists == null)
                {
                    return BadRequest(new { Message = "Invalid email and/or password" });
                }

                var jwtAuth = new JwtAuthHelper(_configuration);
                var token = jwtAuth.GenerateUserToken(userExists);

                return Ok(new
                {
                    Token = token
                });
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Erro interno" });
            }
        }

        [HttpPost]
        [Route("/api/v1/users/new")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDTO request)
        {
            var user = new User
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            _userContext.Users.Add(user);
            await _userContext.SaveChangesAsync();

            var jwtAuth = new JwtAuthHelper(_configuration);
            var token = jwtAuth.GenerateUserToken(user);

            return Ok(new
            {
                Token = token
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/api/v1/users/updatePassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] AuthRequest request)
        {
            try
            {
                var user = await _userContext.Users
                .Where(x => x.Email == request.Email)
                .FirstOrDefaultAsync();

                if (user == null)
                {
                    return NotFound();
                }

                user.Password = request.Password;

                await _userContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Erro interno" });
            }
        }
    }
}