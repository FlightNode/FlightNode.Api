using System;

namespace FlightNode.DataCollection.Domain.Entities
{
    /// <summary>
    /// Models the data needed to display a list of Waterbird Foraging Surveys
    /// </summary>
    public class SurveyListItem
    {
        /// <summary>
        /// Gets or sets the survey identifier.
        /// </summary>
        public Guid SurveyIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the Site Code for the survey location.
        /// </summary>
        public string SiteCode { get; set; }

        /// <summary>
        /// Gets or sets the Site Name for the survey location.
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// Gets or sets the Start Date for the survey.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who submitted the survey.
        /// </summary>
        public string SubmittedBy { get; set; }

        /// <summary>
        /// Gets or sets the status (pending or complete) of the survey data entry.
        /// </summary>
        public string Status { get; set; }
    }
}
