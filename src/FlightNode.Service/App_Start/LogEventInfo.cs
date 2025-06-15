using Newtonsoft.Json;
using System;

namespace FligthNode.Service.App
{
    internal class LogEventInfo
    {
        public Exception Exception { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}