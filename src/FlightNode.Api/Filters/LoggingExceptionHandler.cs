using FlightNode.Common.Exceptions;
using log4net;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace FlightNode.Common.Api.Filters
{
    /// <summary>
    /// Exception handler of last resort, for when the filters handlers are not used.
    /// </summary>
    public class LoggingExceptionHandler : ExceptionHandler, IExceptionHandler
    {
        private ILog _logger;

        public ILog Logger
        {
            get
            {
                return _logger ?? (_logger = LogManager.GetLogger(GetType().FullName));
            }
            set
            {
                _logger = value;
            }
        }


        public override void Handle(ExceptionHandlerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            IHttpActionResult actionResult = new InternalServerErrorResult(context.Request);
            if (context.Exception is DoesNotExistException)
            {
                actionResult = Handle(context.Request, context.Exception as DoesNotExistException);
            }
            else if (context.Exception is UserException)
            {
                actionResult = Handle(context.Request, context.Exception as UserException);
            }
            else if (context.Exception is DomainValidationException)
            {
                actionResult = Handle(context.Request, context.Exception as DomainValidationException);
            }
            else
            {
                actionResult = Handle(context.Request, context.Exception);
            }

            context.Result = actionResult;

            base.Handle(context);
        }

        private IHttpActionResult Handle(HttpRequestMessage request, DoesNotExistException dne)
        {
            Logger.Debug(dne);

            return new NotFoundResult(request);
        }

        private IHttpActionResult Handle(HttpRequestMessage request, UserException ex)
        {
            Logger.Debug(ex);

            var formatter = new JsonMediaTypeFormatter();
            return new BadRequestErrorMessageResult(ex.Message, new JsonContentNegotiator(formatter), request, new[] { formatter });
        }

        private IHttpActionResult Handle(HttpRequestMessage request, DomainValidationException ex)
        {
            Logger.Debug(ex);

            var formatter = new JsonMediaTypeFormatter();
            return new InvalidModelStateResult(ConvertToModelStateErrors(ex), true, new JsonContentNegotiator(formatter), request, new[] { formatter });
        }

        private static ModelStateDictionary ConvertToModelStateErrors(DomainValidationException ex)
        {
            var modelState = new ModelStateDictionary();

            ex.ValidationResults.ToList().ForEach(x =>
            {
                x.MemberNames.ToList().ForEach(y =>
                {
                    modelState.AddModelError(y, x.ErrorMessage);
                });
            });
            return modelState;
        }

        private IHttpActionResult Handle(HttpRequestMessage request, Exception ex)
        {
            Logger.Error(ex);

            return new InternalServerErrorResult(request);
        }

    }
}
