﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBanking.BLL.Providers;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Models;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos.Token;
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
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSendingService _emailSendingService;

        public UsersController(IUserService userService,
            UserManager<User> userManager,
            IMapper mapper,
            SignInManager<User> signInManager,
            IEmailSendingService emailSendingService)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _emailSendingService = emailSendingService;
        }

        [HttpGet]
        [JwtAuthorize]
        public async Task<IActionResult> Get()
        {
            var user = await _userService.GetFromIdentityAsync(User.Identity.Name);

            if (user == null)
                return Unauthorized();

            return Ok(_mapper.Map<User, UserDto>(user.Data));
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

            var twoFactorToken = await _userManager.GenerateTwoFactorTokenAsync(user, ProviderConstansts.UserTwoFactorTokenProvider);

            if (twoFactorToken == null)
                return BadRequest("Can't generate new token");

            await _emailSendingService.SendAsync(user.Email, "Code", twoFactorToken);

            return Ok(new TwoFactorTokenDto
            {
                UserId = user.Id
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