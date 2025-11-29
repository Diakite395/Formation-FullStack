using Api_TaskFlow_DotNet.Data;
using Api_TaskFlow_DotNet.Models.Dtos;
using Api_TaskFlow_DotNet.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_TaskFlow_DotNet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(TaskFlowDbContext db, IUserRepository userRepository, ITokenRepository tokenManager) : ControllerBase
    {
        private readonly TaskFlowDbContext _db = db;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenRepository _tokenManager = tokenManager;

        [HttpGet("{id:guid}")]
        public IActionResult GetUser(Guid id)
        {
            var user = _userRepository.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user.ToUsersResponse());
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetUsers().Select(u => u.ToUsersResponse()).ToList();
            return Ok(users);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromForm] CreateUser request)
        {
            if (!_userRepository.IsUniqueUser(request.Username, request.Email))
            {
                return BadRequest("Username or Email already exists.");
            }

            var user = _userRepository.Create(request);
            var token = _tokenManager.GenerateTokens(user.Username, user.Role);
            return Ok(user.ToUserResponse(token));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromForm] LoginUser request)
        {
            var user = _userRepository.Login(request);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            var token = _tokenManager.GenerateTokens(user.Username, user.Role);
            return Ok(user.ToUserResponse(token));
        }

        [HttpPut("update")]
        public IActionResult Update([FromForm] UpdateUser request)
        {
            var user = _userRepository.GetUser(request.Id);
            if (user != null)
            {
                user.Email = request.Email;
                user.Username = request.Username;
                user.Role = request.Role;

                var updatedUser = _userRepository.Update(user);
                return Ok(updatedUser.ToUsersResponse());
            }
            
            return NotFound();
        }

        [HttpPut("changepassword")]
        public IActionResult ChangePassword([FromForm] string password, Guid userId)
        {
            _userRepository.ChangePassword(password, userId);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var result = _userRepository.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok("User deleted successfully.");
        }

        // [Authorize(Roles = "Admin")]
        [HttpPut("softdelete")]
        public IActionResult SoftDelete([FromForm] Guid id)
        {
            var result = _userRepository.SoftDelete(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok("User soft-deleted successfully.");
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // In a stateless JWT authentication system, logout can be handled on the client side
            // by simply deleting the token. However, if you want to implement token blacklisting,
            // you would need to store the tokens server-side and mark them as invalid upon logout.
            return Ok("Logout successful.");
        }

        [AllowAnonymous]
        [HttpGet("refresh")]
        public IActionResult Refresh([FromForm] string refreshToken)
        {
            var token = _tokenManager.Refresh(refreshToken);

            if (token != null)
                return Ok(token);
            
            return BadRequest("Invalide Refresh token, please try to login");
        }
    }
}
