using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using System.Net.Http;
using System.Web.Http;

namespace FlightNode.Common.UnitTests
{
    public abstract class BaseTester
    {
        protected IFixture Fixture = new Fixture().Customize(new ApiControllerConventions());



        protected static HttpResponseMessage ExecuteHttpAction(IHttpActionResult result)
        {
            return result.ExecuteAsync(new System.Threading.CancellationToken()).Result;
        }



        protected static TModel ReadResult<TModel>(HttpResponseMessage message)
        {
            return message.Content.ReadAsAsync<TModel>().Result;
        }
    }
}
