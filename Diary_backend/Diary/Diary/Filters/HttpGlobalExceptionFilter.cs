using Diary.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Diary.Api.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;

        public HttpGlobalExceptionFilter(IWebHostEnvironment env, ILogger<HttpGlobalExceptionFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            context.Result = context.Exception switch
            {
                ArgumentNullException ex => new BadRequestObjectResult(new BadRequestObjectResultDetails(context, ex)),
                ArgumentException ex => new BadRequestObjectResult(new BadRequestObjectResultDetails(context, ex)),
                DomainException ex => new BadRequestObjectResult(new BadRequestObjectResultDetails(context, ex)),
                ValidationException ex => new BadRequestObjectResult(new BadRequestObjectResultDetails(context, ex)),
                _ => new ObjectResult(new
                {
                    Messages = new[] { "An error occur.Try it again." },
                    DeveloperMessage = _env.IsDevelopment() ? context.Exception.ToString() : null
                })
                { StatusCode = StatusCodes.Status500InternalServerError }
            };

            context.ExceptionHandled = true;
        }

        public class BadRequestObjectResultDetails : ValidationProblemDetails
        {
            private BadRequestObjectResultDetails(ActionContext context, params string[] errors)
            {
                Instance = context.HttpContext.Request.Path;
                Status = StatusCodes.Status400BadRequest;
                Detail = "Please refer to the errors property for additional details.";
                Errors.Add("validations", errors);
            }

            public BadRequestObjectResultDetails(ActionContext context, Exception ex) : this(context, ex.Message)
            {
            }

            public BadRequestObjectResultDetails(ActionContext context, ValidationException ex) : this(context,
                ex.Errors.Select(x => x.ToString()).ToArray())
            {
            }
        }
    }
}
