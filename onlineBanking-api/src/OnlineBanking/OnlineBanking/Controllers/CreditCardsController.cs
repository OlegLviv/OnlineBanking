using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Models;
using OnlineBanking.Core.Models.DomainModels.User;
using OnlineBanking.Core.Models.Dtos.User;
using OnlineBanking.Filters.AuthorizationFilters;

namespace OnlineBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [JwtAuthorize]
    public class CreditCardsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreditCardsController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetCreditCars()
        {
            var userHolder = await _userService.GetFromIdentityAsync(User.Identity.Name);

            if (userHolder == null)
                return Unauthorized();

            if (userHolder.Status == DataHolderStatus.Unauthorized)
                return Unauthorized();

            if (userHolder.Status == DataHolderStatus.Failure)
                return BadRequest(userHolder.Message);

            return Ok(_mapper.Map<List<CreditCard>, List<CreditCardDto>>(userHolder.Data.CreditCards));
        }
    }
}
