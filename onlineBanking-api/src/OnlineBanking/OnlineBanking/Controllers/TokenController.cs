using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineBanking.BLL.Providers;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos.Token;
using OnlineBanking.Core.Models.Dtos.User;

namespace OnlineBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSendingService _emailSendingService;
        private readonly string _tokenIssuer,
            _tokenAudience,
            _tokenLifetime,
            _tokenKey;

        public TokenController(IConfiguration configuration,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSendingService emailSendingService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenIssuer = configuration["Issuer"];
            _tokenAudience = configuration["Audience"];
            _tokenLifetime = configuration["Lifetime"];
            _tokenKey = configuration["Key"];
            _emailSendingService = emailSendingService;
        }

        [HttpGet("{userId}/{code}")]
        public async Task<IActionResult> Get(Guid? userId, string code)
        {
            if (userId == null || string.IsNullOrWhiteSpace(code))
                return BadRequest("Incorrect user id or code");

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return BadRequest("Incorrect user id");

            if (!await _userManager.VerifyTwoFactorTokenAsync(user, ProviderConstansts.UserTwoFactorTokenProvider, code)
            )
                return BadRequest("Incorrect security code");

            var userRoles = await _userManager.GetRolesAsync(user);

            var jwt = new JwtSecurityToken(
                issuer: _tokenIssuer,
                audience: _tokenAudience,
                notBefore: DateTime.UtcNow,
                claims: GetIdentity(user.UserName, userRoles).Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(double.Parse(_tokenLifetime))),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenKey)), SecurityAlgorithms.HmacSha256));

            return Ok(new TokenDto { AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt) });
        }

        [HttpPost("sendUser2fa")]
        public async Task<IActionResult> SendUserTwoFactorToken([FromBody] LoginUserDto logInDto)
        {
            var user = await FindUserByUserNameOrEmail(logInDto);

            if (user == null)
                return BadRequest("Incorrect login or password");

            if (!(await _signInManager.CheckPasswordSignInAsync(user, logInDto.Password, true)).Succeeded)
                return BadRequest("Incorrect password");

            var twoFactorToken = await _userManager.GenerateTwoFactorTokenAsync(user, ProviderConstansts.UserTwoFactorTokenProvider);

            if (twoFactorToken == null)
                return BadRequest("Can't generate new token");

            await _emailSendingService.SendAsync(user.Email, "Code", twoFactorToken);

            return Ok(new TwoFactorTokenDto
            {
                UserId = user.Id
            });
        }

        private static ClaimsIdentity GetIdentity(string userName, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
            };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        private async Task<User> FindUserByUserNameOrEmail(LoginUserDto model)
            => await _userManager
                .Users
                .FirstOrDefaultAsync(user => user.Email == model.UserName || user.UserName == model.UserName);
    }
}