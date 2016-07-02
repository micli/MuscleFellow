using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MuscleFellow.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MuscleFellow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MuscleFellow.API.Utils;
using MuscleFellow.API.Models;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.IdentityModel.Tokens;
using MuscleFellow.API.JWT;
using Microsoft.Extensions.Options;
using System.Text;

namespace MuscleFellow.API.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ILogger _logger;
        IOptions<WebApiSettings> _settings;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ICartItemRepository cartItemRepository,
            ILoggerFactory loggerFactory,
            IOptions<WebApiSettings> settings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cartItemRepository = cartItemRepository;
            _logger = loggerFactory.CreateLogger<AccountController>();
            _settings = settings;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return Ok();
            var user = await _userManager.FindByEmailAsync(id);
            JsonResult result = new JsonResult(user);
            return result;
        }
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] LoginAPIModel registerModel)
        {
            var user = new ApplicationUser { UserName = registerModel.UserID, Email = registerModel.UserID };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                _logger.LogInformation(3, "User created a new account with password.");
                return Ok();
            }

            // If we got this far, something failed.
            return ResponseHelper.Unauthorized();
        }
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginAPIModel loginModel)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
                    loginModel.UserID, loginModel.Password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation(1, "User logged in.");

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.SecretKey));
                var options = new TokenProviderOptions
                {
                    Audience = "MuscleFellowAudience",
                    Issuer = "MuscleFellow",
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                };
                TokenProvider tpm = new TokenProvider(options);
                TokenEntity token = await tpm.GenerateToken(HttpContext, loginModel.UserID, loginModel.Password);
                if(null != token)
                    return new JsonResult(token);
                else
                     return NotFound();
            }

            if (result.IsLockedOut)
            {
                _logger.LogWarning(2, "User account locked out.");
                return Ok("Lockout");
            }
            else
            {
                _logger.LogWarning(2, "Invalid login attempt.");
                return Ok("Invalid login attempt.");
            }
        }
        // POST: /Account/LogOff
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return Ok();
        }
    }
}
