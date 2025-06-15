using System;
using System.Web.Http;
using FlightNode.Common.Exceptions;
using System.Web.Http.ModelBinding;
using System.Linq;
using log4net;
using Flurl;
using FlightNode.Common.Api.Models;
using System.Threading.Tasks;
using System.Net;

namespace FligthNode.Common.Api.Controllers
{
    public abstract class LoggingController : ApiController
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
        // TODO: removall of this try catch stuff and just let the filters catch the exceptions
        protected async Task<IHttpActionResult> WrapWithTryCatchAsync(Func<Task<IHttpActionResult>> func)
        {
            try
            {
                return await func();
            }
            catch (DoesNotExistException dne)
            {
                return Handle(dne);
            }
            catch (UserException uex)
            {
                return Handle(uex);
            }
            catch (DomainValidationException dex)
            {
                return Handle(dex);
            }
            catch (Exception ex)
            {
                return Handle(ex);
            }
        }

        protected IHttpActionResult WrapWithTryCatch(Func<IHttpActionResult> func)
        {
            try
            {
                return func();
            }
            catch(DoesNotExistException dne)
            {
                return Handle(dne);
            }
            catch (UserException uex)
            {
                return Handle(uex);
            }
            catch (DomainValidationException dex)
            {
                return Handle(dex);
            }
            catch (Exception ex)
            {
                return Handle(ex);
            }
        }

        protected IHttpActionResult Handle(DoesNotExistException dne)
        {
            Logger.Debug(dne);

            return Content(System.Net.HttpStatusCode.NotFound, new MessageModel(dne.Message));
        }

        protected IHttpActionResult Handle(UserException ex)
        {
            Logger.Debug(ex);

            return BadRequest(ex.Message);
        }

        protected IHttpActionResult Handle(DomainValidationException ex)
        {
            Logger.Debug(ex);

            return BadRequest(ConvertToModelStateErrors(ex));
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

        protected IHttpActionResult Handle(Exception ex)
        {
            Logger.Error(ex);

            return InternalServerError();
        }


        protected virtual IHttpActionResult NoContent()
        {
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        protected virtual IHttpActionResult MethodNotAllowed()
        {
            return StatusCode(System.Net.HttpStatusCode.MethodNotAllowed);
        }


        // TODO: replace any code using this with a call to CreatedAtRoute()
        protected IHttpActionResult Created<TModel>(TModel input, string id)
        {
            var locationHeader = this.Request
                .RequestUri
                .ToString()
                .AppendPathSegment(id);

            return Created(locationHeader, input);
        }


        protected IHttpActionResult Unprocessable()
        {
            return StatusCode((HttpStatusCode)422);
        }
    }
}
