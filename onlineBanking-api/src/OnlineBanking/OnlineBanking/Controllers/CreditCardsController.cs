using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.BLL.Services.Abstract;
using OnlineBanking.Core.Models;
using OnlineBanking.Core.Models.DomainModels.CreditCard;
using OnlineBanking.Core.Models.Dtos.CreditCard;
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
        private readonly ICreditCardService _creditCardService;

        public CreditCardsController(IUserService userService, IMapper mapper, ICreditCardService creditCardService)
        {
            _userService = userService;
            _mapper = mapper;
            _creditCardService = creditCardService;
        }

        #region GET

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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                return BadRequest("Invalid id");

            var userIdHolder = await _userService.GetUserId(User.Identity.Name);

            if (userIdHolder.Status == DataHolderStatus.Unauthorized)
                return Unauthorized();

            var creditCardHolder = await _creditCardService.GetById(id.Value, new Guid(userIdHolder.Data));

            if (creditCardHolder.Status == DataHolderStatus.Failure)
                return BadRequest(creditCardHolder.Message);

            return Ok(_mapper.Map<CreditCard, CreditCardDto>(creditCardHolder.Data));
        }

        [HttpGet("costs/{id}/{itemPerPage:int}/{page:int}")]
        public async Task<IActionResult> GetCosts(Guid id, int itemPerPage = 10, int page = 1)
        {
            if (id == Guid.Empty)
                return BadRequest("Invalid id");

            var logsHolder = await _creditCardService.GetTransactionMoneyLogAsync(id, itemPerPage, page);

            if (logsHolder.Status == DataHolderStatus.Failure)
                return BadRequest(logsHolder.Message);

            return Ok(logsHolder.Data);
        }

        #endregion

        #region POST

        [HttpPost("createOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateCreditCardOrderDto model)
        {
            var user = await _userService.GetFromIdentityAsync(User.Identity.Name);

            if (user.Status == DataHolderStatus.Unauthorized)
                return Unauthorized();

            var createResult = await _creditCardService.CreateOrderAsync(model, user.Data);

            if (createResult.Status == DataHolderStatus.Failure)
                return BadRequest(createResult.Message);

            return Ok(_mapper.Map<CreditCardOrder, CreditCardOrderDto>(createResult.Data));
        }

        [HttpPost("sendMoneyToCard")]
        public async Task<IActionResult> SendMoneyToCard([FromBody] SendMoneyDto sendMoneyDto)
        {
            var userIdHolder = await _userService.GetUserId(User.Identity.Name);

            if (userIdHolder.Status == DataHolderStatus.Unauthorized)
                return Unauthorized();

            var sendMoneyHolder = await _creditCardService.SendMoneyAsync(sendMoneyDto, new Guid(userIdHolder.Data));

            if (sendMoneyHolder.Status == DataHolderStatus.Failure)
                return BadRequest(sendMoneyHolder.Message);

            return Ok(_mapper.Map<CreditCard, CreditCardDto>(sendMoneyHolder.Data));
        }

        #endregion

        #region PUT

        [HttpPut("changePin")]
        public async Task<IActionResult> ChangePin([FromBody] ChangePinDto pinDto)
        {
            if (pinDto.NewPin < 1000 || pinDto.NewPin > 9999)
                return BadRequest("Invalid pin");

            var userIdHolder = await _userService.GetUserId(User.Identity.Name);

            if (userIdHolder.Status == DataHolderStatus.Unauthorized)
                return Unauthorized();

            var cardHolder = await _creditCardService.ChangePinAsync(pinDto, new Guid(userIdHolder.Data));

            if (cardHolder.Status == DataHolderStatus.Failure)
                return BadRequest(cardHolder.Message);

            return Ok(_mapper.Map<CreditCard, CreditCardDto>(cardHolder.Data));
        }

        [HttpPut("changeCreditLimit")]
        public async Task<IActionResult> ChangeCreditLimit([FromBody] ChangeCreditLimitDto creditLimitDto)
        {
            var userIdHolder = await _userService.GetUserId(User.Identity.Name);

            if (userIdHolder.Status == DataHolderStatus.Unauthorized)
                return Unauthorized();

            var cardHolder = await _creditCardService.ChangeCreditLimitAsync(creditLimitDto, new Guid(userIdHolder.Data));

            if (cardHolder.Status == DataHolderStatus.Failure)
                return BadRequest(cardHolder.Message);

            return Ok(_mapper.Map<CreditCard, CreditCardDto>(cardHolder.Data));
        }
        #endregion
    }
}
