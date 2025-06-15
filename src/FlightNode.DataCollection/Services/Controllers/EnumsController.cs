using FlightNode.DataCollection.Domain.Entities;
using FlightNode.DataCollection.Domain.Interfaces.Persistence;
using FligthNode.Common.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace FlightNode.DataCollection.Services.Controllers
{
    /// <summary>
    /// Used to retrieve various "enum" attributes used on data forms
    /// </summary>
    [RoutePrefix("api/v1/enums")]
    [Authorize]
    public class EnumsController : LoggingController
    {

        private readonly IEnumRepository _repository;

        public EnumsController(IEnumRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            _repository = repository;
        }

        [HttpGet]
        [Route("weather")]
        public async Task<IHttpActionResult> Weather()
        {
            // TODO: either add 500 error handling here or ensure that it is covered by a filter

            var values = await _repository.GetWeather();

            return Ok(values ?? new List<Weather>());
        }

        [HttpGet]
        [Route("waterheights")]
        public async Task<IHttpActionResult> WaterHeights()
        {
            var values = await _repository.GetWaterHeights();

            return Ok(values ?? new List<WaterHeight>());
        }

        [HttpGet]
        [Route("disturbancetypes")]
        public async Task<IHttpActionResult> DisturbanceTypes()
        {
            var values = await _repository.GetDisturbanceTypes();

            return Ok(values ?? new List<DisturbanceType>());
        }


        [HttpGet]
        [Route("habitattypes")]
        public async Task<IHttpActionResult> HabitatTypes()
        {
            var values = await _repository.GetHabitatTypes();

            return Ok(values ?? new List<HabitatType>());
        }


        [HttpGet]
        [Route("feedingsuccessrates")]
        public async Task<IHttpActionResult> FeedingSuccessRates()
        {
            var values = await _repository.GetFeedingSuccessRates();

            return Ok(values ?? new List<FeedingSuccessRate>());
        }


        [HttpGet]
        [Route("activitytypes")]
        public async Task<IHttpActionResult> ActivityTypes()
        {
            var values = await _repository.GetSurveyActivities();

            return Ok(values ?? new List<SurveyActivity>());
        }



        [HttpGet]
        [Route("siteassessments")]
        public async Task<IHttpActionResult> SiteAssessments()
        {
            var values = await _repository.GetSiteAssessments();

            return Ok(values ?? new List<SiteAssessment>());
        }


        [HttpGet]
        [Route("vantagepoints")]
        public async Task<IHttpActionResult> VantagePoints()
        {
            var values = await _repository.GetVantagePoints();

            return Ok(values ?? new List<VantagePoint>());
        }

        [HttpGet]
        [Route("accesspoints")]
        public async Task<IHttpActionResult> AccessPoints()
        {
            var values = await _repository.GetAccessPoints();

            return Ok(values ?? new List<AccessPoint>());
        }

        [HttpGet]
        [Route("windspeeds")]
        public async Task<IHttpActionResult> WindSpeeds()
        {
            var values = await _repository.GetWindSpeeds();

            return Ok(values ?? new List<WindSpeed>());
        }


        [HttpGet]
        [Route("winddirections")]
        public async Task<IHttpActionResult> WindDirections()
        {
            var values = await _repository.GetWindDirections();

            return Ok(values ?? new List<WindDirection>());
        }
    }
}
