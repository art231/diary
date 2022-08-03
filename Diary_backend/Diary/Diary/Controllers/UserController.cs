using Diary.Application.Interfaces;
using Diary.Application.Queries.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Diary.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class UserController : DiaryControllerBase
    {
        public UserController(IBus bus) : base(bus)
        {
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            return Ok(await Bus.Send(new GetUserQuery(Guid.Parse(userId))));
        }
    }
}
