using Provider.Base.REST.Enums;
using RestSharp;
using System.Collections;

namespace Provider.Base.REST
{
    public abstract class BaseRestClient
    {
        protected RestFormat format;
        protected string baseUri;
        protected RestClient restClient;

        public BaseRestClient(string baseUri, RestFormat format) {
            this.baseUri = baseUri;
            this.format = format;

            restClient = new RestClient(baseUri);
        }

        protected virtual RestRequest GetRequest(RequestMethod method, string resource, RestParams parameters, RestObject body = null) {
            RestRequest request = new RestRequest(resource, (Method)method);

            ProcessParameters(request, parameters);

            if (body != null) {
                request.AddBody(body);
            }

            return request;
        }

        protected virtual void ProcessParameters(RestRequest request, RestParams parameters) {
            if (parameters == null) {
                return;
            }

            Hashtable paramList = parameters.GetMap();

            foreach (string key in paramList.Keys) {
                request.AddParameter(key, paramList[key]);
            }
        }

        public virtual T Execute<T>(RequestMethod method, string resource, RestParams parameters = null, RestObject body = null) where T : RestObject, new() {
            RestRequest request = GetRequest(method, resource, parameters, body);
            request.RequestFormat = (DataFormat)format;
            
            return restClient.Execute<T>(request).Data;
        }

        public virtual RestResponse ExecuteRAW(RequestMethod method, string resource, RestParams parameters, RestObject body = null) {
            RestRequest request = GetRequest(method, resource, parameters, body);

            return RestResponse.InitializeFromIResponse(restClient.Execute(request));
        }

        public T ExecuteGET<T>(string resource, RestParams parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.GET, resource, parameters);
        }

        public T ExecutePOST<T>(string resource, RestParams parameters, T body = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.POST, resource, parameters, body);
        }

        public T ExecutePUT<T>(string resource, RestParams parameters, T body = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.PUT, resource, parameters, body);
        }

        public T ExecuteDELETE<T>(string resource, RestParams parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.DELETE, resource, parameters);
        }

        public T ExecuteOPTIONS<T>(string resource, RestParams parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.OPTIONS, resource, parameters);
        }

        public T ExecutePATCH<T>(string resource, RestParams parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.PATCH, resource, parameters);
        }

        public T ExecuteMERGE<T>(string resource, RestParams parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.MERGE, resource, parameters);
        }

        public T ExecuteHEAD<T>(string resource, RestParams parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.HEAD, resource, parameters);
        }
    }
}
