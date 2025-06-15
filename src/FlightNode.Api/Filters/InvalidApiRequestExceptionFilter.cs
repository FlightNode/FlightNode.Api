using Exceptions;
using log4net;
using System;
using System.Net;
using System.Web.Http.Filters;

namespace FlightNode.Common.Api.Filters
{
    public class InvalidApiRequestExceptionFilter : ExceptionFilterAttribute
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

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }


            var ex = context.Exception as InvalidApiRequestException;

            if (ex == null)
            {
                return;
            }

            LogErrorDetails(ex);

            context.Response = new System.Net.Http.HttpResponseMessage(HttpStatusCode.InternalServerError);
            context.Response.Content = null;
        }


        private void LogErrorDetails(InvalidApiRequestException ex)
        {
            var errors = string.Empty;

            foreach (var item in ex.Errors)
            {
                errors += item + ". ";
            }

            Logger.ErrorFormat("SendGrid Invalid API Request: {0}", errors);
        }
    }
}
