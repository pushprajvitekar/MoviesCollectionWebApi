using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Domain.Exceptions;

namespace MovieCollectionWebApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public ExceptionFilter()
        {
        }
        public void OnException(ExceptionContext context)
        {
            Logger?.Error(context.Exception, context.Exception.Message);
            switch (context.Exception)
            {
                case ArgumentException:
                    context.Result = new BadRequestResult();
                    break;
                case InvalidOperationException:
                    context.Result = new NotFoundResult();
                    break;
                case InfrastructureException:
                    var iex = context.Exception as DomainException;
                    var scode = iex?.ErrorCode ?? StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult("Infrastucture error") { StatusCode = scode };
                    break;
                case DomainException:
                    var dex = context.Exception as DomainException;
                    var dcode = dex?.ErrorCode ?? StatusCodes.Status400BadRequest;
                    context.Result = new ObjectResult(dex?.Message ?? "Domain error") { StatusCode = dcode };
                    break;
                case Exception:
                    context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
                    break;

            }
        }
    }
}
