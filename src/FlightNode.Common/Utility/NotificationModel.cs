using System;

namespace FlightNode.Common.Utility
{

    public class NotificationModel
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="NotificationModel"/>, with default "From" information.
        /// </summary>
        /// <param name="to">E-mail recipient</param>
        /// <param name="subject">E-mail subject</param>
        /// <param name="body">E-mail body</param>
        public NotificationModel(string to, string subject, string body)
        {
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }
            if (to.Trim().Length == 0)
            {
                throw new ArgumentException("to cannot be an empty string", "to");
            }
            if (subject == null)
            {
                throw new ArgumentNullException("subject");
            }
            if (subject.Trim().Length == 0)
            {
                throw new ArgumentException("subject cannot be an empty string", "subject");
            }
            if (body == null)
            {
                throw new ArgumentNullException("body");
            }
            if (body.Trim().Length == 0)
            {
                throw new ArgumentException("body cannot be an empty string", "body");
            }

            FromEmail = Properties.Settings.Default.FromEmail;
            FromName = Properties.Settings.Default.FromName;

            To = to;
            Subject = subject;
            Body = body;
        }
    }
}
