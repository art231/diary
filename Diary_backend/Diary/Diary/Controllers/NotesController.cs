using Diary.Application.Commands.Notes;
using Diary.Application.Interfaces;
using Diary.Application.Queries.Notes;
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
    public class NotesController : DiaryControllerBase
    {
        public NotesController(IBus bus) : base(bus)
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(NotesWithoutUserId request)
        {
            var userId = GetUserId();
            var command = new NotesCreateCommand(Guid.Parse(userId),
                request.InitialDate,
                request.Title,
                request.Description);
            
            await Bus.Send(command);
            return Ok();
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(NotesUpdateCommand request)
        {
            await Bus.Send(request);
            return Ok();
        }
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNotes([FromQuery]GetListNotesQuery request)
        {
            return Ok(await Bus.Send(request));
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetNote([FromQuery]GetNotesQuery request)
        {
            return Ok(await Bus.Send(request));
        }
    }

    public sealed record NotesWithoutUserId
    {
        /// <summary>
        /// дата назначения
        /// </summary>
        public DateTime InitialDate { get; init; }
        /// <summary>
        /// Тема
        /// </summary>
        public string Title { get; init; }
        /// <summary>
        /// Описание
        /// </summary>
        #nullable enable
        public string? Description { get; init; }
    }
}
