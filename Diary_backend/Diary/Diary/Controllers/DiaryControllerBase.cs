using Diary.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Diary.Api.Controllers
{

    public abstract class DiaryControllerBase : ControllerBase
    {
        protected DiaryControllerBase(IBus bus)
        {
            Bus = bus;
        }

        protected IBus Bus { get; }

        protected IActionResult Result(object? value)
        {
            return value switch
            {
                null => NotFound(),
                bool ok when !ok => BadRequest(),
                bool ok when ok => Ok(),
                _ => Ok(value)
            };
        }
        protected string GetUserId()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            return claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}
