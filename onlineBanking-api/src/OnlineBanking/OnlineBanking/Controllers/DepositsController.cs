using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Models;
using OnlineBanking.Core.Models.Dtos.Deposit;
using OnlineBanking.Filters.AuthorizationFilters;

namespace OnlineBanking.Controllers
{
    [Route("/api/[controller]"), ApiController]
    [JwtAuthorize]
    public class DepositsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDepositService _depositService;

        public DepositsController(IDepositService depositService, IUserService userService)
        {
            _depositService = depositService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userIdHolder = await _userService.GetUserId(User.Identity.Name);

            if (userIdHolder.Status == DataHolderStatus.Unauthorized)
                return Unauthorized();

            return Ok((await _depositService
                .GetDepositsAsync(new Guid(userIdHolder.Data)))
                .Data);
        }

        [HttpGet("DepositTypes/{currency}")]
        public async Task<IActionResult> GetDepositTypes(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                return BadRequest("Incorrect currency");

            var userIdHolder = await _userService.GetUserId(User.Identity.Name);

            if (userIdHolder.Status == DataHolderStatus.Unauthorized)
                return Unauthorized();

            var depositTypesHolder = await _depositService.GetDepositTypesAsync(currency, new Guid(userIdHolder.Data));

            if (depositTypesHolder.Status == DataHolderStatus.Failure)
                return BadRequest(depositTypesHolder.Message);

            return Ok(depositTypesHolder.Data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateDepositDto createDepositDto)
        {
            var userIdHolder = await _userService.GetUserId(User.Identity.Name);

            if (userIdHolder.Status == DataHolderStatus.Unauthorized)
                return Unauthorized();

            var createdHolder = await _depositService.CreateDepositAsync(createDepositDto, new Guid(userIdHolder.Data));

            if (createdHolder.Status == DataHolderStatus.Failure)
                return BadRequest(createdHolder.Message);

            return Ok(createdHolder.Data);
        }
    }
}
