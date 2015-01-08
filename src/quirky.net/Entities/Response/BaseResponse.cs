using System;
using System.Collections.Generic;

namespace quirky.net.Entities.Response
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Headers = new Dictionary<string, IEnumerable<string>>();
        }

        public List<string> errors { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }


        public TimeSpan Duration { get; set; }
        public DateTime StartTimeStamp { get; set; }
        public DateTime EndTimeStamp { get; set; }
        public TimeSpan ReadTimeSpan { get; set; }
        public StatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public Dictionary<string, IEnumerable<string>> Headers { get; protected set; }

        public string Content
        {
            get
            {
                var msg = string.Format("Success:{0}\nStatusCode:{1}\nDuration:{2}", Success, StatusCode,
                    Duration.TotalMilliseconds);
                if (Headers != null)
                {
                    foreach (var header in Headers)
                    {
                        foreach (var val in header.Value)
                        {
                            msg = string.Format("{0}\n{1}:{2}", msg, header.Key, val);
                        }
                    }
                }
                msg = string.Format("{0}\n{1}", msg, Message);

                return msg;
            }
        }

        public IEnumerable<string> Header(string name)
        {
            if (Headers == null) return null;
            if (!Headers.ContainsKey(name)) return null;
            return Headers[name];

        }
    }
}




