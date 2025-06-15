using System;

namespace FlightNode.Common.Exceptions
{
    public class ServerException : Exception
    {
        public const string MESSAGE = "A server error occurred.";

        public DatabaseExceptionContent Content { get; private set; }

        protected ServerException(string message) : base(message)
        {
        }

        protected ServerException() : base(MESSAGE)
        {

        }

        protected ServerException(Exception inner)    
            : base(MESSAGE, inner)
        {
        }

        public static ServerException UpdateFailed<TModel>(string description, int id)
        {
            return new ServerException
            {
                Content = new DatabaseExceptionContent
                {
                    Id = id,
                    Action = "Update",
                    Description = description,
                    ModelType = typeof(TModel)
                }
            };
        }
       
        public static ServerException HandleException<TModel>(Exception exception, string action, int? id = null)
        {
            return new ServerException(exception)
            {
                Content = new DatabaseExceptionContent
                {
                    Id = id,
                    Action = action,
                    Description = exception.Message,
                    ModelType = typeof(TModel)
                }
            };
        }

    }

}
