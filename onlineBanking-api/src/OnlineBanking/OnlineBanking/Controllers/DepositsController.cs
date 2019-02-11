using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Models;
using OnlineBanking.Core.Models.DomainModels.Deposit;
using OnlineBanking.Core.Models.Dtos.Deposit;

namespace OnlineBanking.Controllers
{
    [Route("/api/[controller]"), ApiController]
    public class DepositsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDepositService _depositService;
        private readonly IMapper _mapper;

        public DepositsController(IDepositService depositService, IUserService userService, IMapper mapper)
        {
            _depositService = depositService;
            _userService = userService;
            _mapper = mapper;
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

            return Ok(_mapper.Map<Deposit, DepositTypeDto>(createdHolder.Data));
        }
    }
}
