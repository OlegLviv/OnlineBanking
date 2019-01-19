using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Models;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos;
using OnlineBanking.Core.Models.Dtos.User;
using OnlineBanking.Core.Models.Dtos.User.Register;
using OnlineBanking.Filters.AuthorizationFilters;

namespace OnlineBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, UserManager<User> userManager, IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [JwtAuthorize]
        public async Task<IActionResult> Get()
        {
            var user = await _userService.GetFromIdentityAsync(User.Identity.Name);

            if (user == null)
                return Unauthorized();

            return Ok(_mapper.Map<DataHolder<User>, DataHolder<UserDto>>(user));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
           var registerResult = await _userService.RegisterUserAsync(registerUserDto);

           if (registerResult.Status == DataHolderStatus.Failure)
               return BadRequest(registerResult.Message);

            return Ok(_mapper.Map<DataHolder<User>, DataHolder<UserDto>>(registerResult));
        }
    }
}