using Microsoft.AspNetCore.Mvc;
using Tdp.Application.Contracts.Application;
using Tdp.Domain.Dtos.Auth;

namespace Tdp.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var result = await _auth.Register(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var result = await _auth.Login(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

            [HttpDelete("{userId}")]
            public async Task<IActionResult> DeleteAccount(Guid userId)
            {
                if (userId == Guid.Empty)
                    return BadRequest("Invalid user id.");

                var exists = await _auth.UserExists(userId);
                if (!exists)
                    return NotFound("User not found.");

                if (!Request.Headers.TryGetValue("X-User-Id", out var actorHeader))
                    return StatusCode(403, "Missing user context.");

                if (!Guid.TryParse(actorHeader, out var actorUserId))
                    return BadRequest("Invalid user context.");

                if (actorUserId != userId)
                    return StatusCode(403, "You can only delete your own account.");

                await _auth.DeleteUser(userId);

                return NoContent();
            }
        


    }
}
