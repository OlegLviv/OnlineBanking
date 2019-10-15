using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineBanking.BLL.Providers;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Models;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos.Token;
using OnlineBanking.Core.Models.Dtos.User;
using OnlineBanking.Core.Models.Dtos.User.Register;
using OnlineBanking.Filters.AuthorizationFilters;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSendingService _emailSendingService;
        private readonly IConfiguration _configuration;

        public UsersController(IUserService userService,
            UserManager<User> userManager,
            IMapper mapper,
            SignInManager<User> signInManager,
            IEmailSendingService emailSendingService,
            IConfiguration configuration)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _emailSendingService = emailSendingService;
            _configuration = configuration;
        }

        [HttpGet("getCurrent")]
        [JwtAuthorize]
        public async Task<IActionResult> GetCurrent()
        {
            var userHolder = await _userService.GetFromIdentityAsync(User.Identity.Name);

            if (userHolder == null)
                return Unauthorized();

            if (userHolder.Status == DataHolderStatus.Unauthorized)
                return Unauthorized();

            if (userHolder.Status == DataHolderStatus.Failure)
                return BadRequest(userHolder.Message);

            return Ok(_mapper.Map<User, UserDto>(userHolder.Data));
        }

        [HttpPost("sendUser2fa")]
        public async Task<IActionResult> SendUserTwoFactorToken([FromBody] LoginUserDto logInDto)
        {
            var user = await _userManager
                .Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == logInDto.PhoneNumber);

            if (user == null)
                return BadRequest("Incorrect login or password");

            if (!(await _signInManager.CheckPasswordSignInAsync(user, logInDto.Password, true)).Succeeded)
                return BadRequest("Incorrect password");

            var twoFactorToken = await _userManager.GenerateTwoFactorTokenAsync(user, ProviderConstants.UserTwoFactorTokenProvider);

            if (twoFactorToken == null)
                return BadRequest("Can't generate new token");

            await _emailSendingService.SendAsync(user.Email, "Code", twoFactorToken);

            return Ok(new TwoFactorDto
            {
                UserId = user.Id,
                Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                ExpiredAfter = int.Parse(_configuration["TwoFactorLifeTime"])
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            var registerResult = await _userService.RegisterUserAsync(registerUserDto);

            if (registerResult.Status == DataHolderStatus.Failure)
                return BadRequest(registerResult.Message);

            return Ok(_mapper.Map<User, UserDto>(registerResult.Data));
        }
    }
}