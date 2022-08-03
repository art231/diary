using Diary.Application.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Diary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private IMediator? mediator;
        protected IMediator Mediator => this.mediator ??= this.HttpContext.RequestServices.GetRequiredService<IMediator>();
        public AuthController()
        {
        }
        /// <summary>
        /// Вход для администратора
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /token
        ///     {
        ///        "phoneNumber": "+79660813254",
        ///        "password": "Aa,1234567"
        ///     }
        ///
        /// </remarks>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<TokenViewModel> Register([FromBody] AuthCommand authRequest)
        {
            return await this.Mediator.Send(authRequest);
        }
    }
    
}
