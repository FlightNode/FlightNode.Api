using FlightNode.Common.BaseClasses;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightNode.DataCollection.Domain.Entities
{
    /// <summary>
    /// Models User data as needed for data collection.
    /// </summary>
    /// <remarks>
    /// This class should only be used for reading data in queries. It should not be used for 
    /// creating, updating, or deleting users. See the "Identity" project for those needs.
    /// </remarks>
    public class User : IEntity
    {
        /// <summary>
        /// Gets or sets the primary key ID value.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Given ("first") name.
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// Gets or sets the Family ("last") name.
        /// </summary>
        public string FamilyName { get; set; }
    }
}
