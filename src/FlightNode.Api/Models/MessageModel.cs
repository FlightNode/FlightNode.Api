
namespace FlightNode.Common.Api.Models
{
    /// <summary>
    /// Useful when you want to send a simple string message back to the client, as JSON.
    /// </summary>
    public class MessageModel
    {
        /// <summary>
        /// Message for the client.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Creates a new <see cref="MessageModel"/>.
        /// </summary>
        /// <param name="message">Message for the client.</param>
        public MessageModel(string message)
        {
            Message = message ?? string.Empty;
        }
    }
}
