using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Provider.Base.REST
{
    public class RestResponse : IRestResponse
    {
        public IRestRequest Request { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public string ContentEncoding { get; set; }
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public byte[] RawBytes { get; set; }
        public Uri ResponseUri { get; set; }
        public string Server { get; set; }

        public IList<RestResponseCookie> Cookies { get; set; }

        public IList<Parameter> Headers { get; set; }

        public ResponseStatus ResponseStatus { get; set; }

        public string ErrorMessage { get; set; }

        public Exception ErrorException { get; set; }

        public static RestResponse InitializeFromIResponse(IRestResponse response) {
            return new RestResponse() {
                Request = response.Request,
                ContentType = response.ContentType,
                ContentLength = response.ContentLength,
                ContentEncoding = response.ContentEncoding,
                Content = response.Content,
                StatusCode = response.StatusCode,
                StatusDescription = response.StatusDescription,
                RawBytes = response.RawBytes,
                ResponseUri = response.ResponseUri,
                Server = response.Server,
                Headers = response.Headers,
                Cookies = response.Cookies,
                ResponseStatus = response.ResponseStatus,
                ErrorMessage = response.ErrorMessage,
                ErrorException = response.ErrorException
            };
        }
    }
}
