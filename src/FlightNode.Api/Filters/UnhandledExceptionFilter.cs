using log4net;
using System;
using System.Web.Http.Filters;


namespace FlightNode.Common.Api.Filters
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
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

            
            Logger.Error(context.Request.RequestUri, context.Exception);
        }
    }
}
