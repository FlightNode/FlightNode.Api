namespace FlightNode.Common.Api.Models
{
    /// <summary>
    /// This model is used when an API needs to present a very simple list of key-vaue pairs, e.g. for a select tag or list box.
    /// </summary>
    public class SimpleListItem
    {
        /// <summary>
        /// List item identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// List item description.
        /// </summary>
        public string Value { get; set; }
    }
}
