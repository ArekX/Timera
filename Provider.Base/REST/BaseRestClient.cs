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

        protected virtual RestRequest GetRequest(RequestMethod method, string resource, object parameters, RestObject body = null) {
            RestRequest request = new RestRequest(resource, (Method)method);

            ProcessParameters(request, parameters);

            if (body != null) {
                request.AddBody(body);
            }

            return request;
        }

        protected virtual void ProcessParameters(RestRequest request, object parameters) {
            if (parameters == null || !(parameters is Hashtable)) {
                return;
            }

            Hashtable paramList = (Hashtable)parameters;

            foreach (string key in paramList.Keys) {
                request.AddParameter(key, paramList[key]);
            }
        }

        public virtual T Execute<T>(RequestMethod method, string resource, object parameters = null, RestObject body = null) where T : RestObject, new() {
            RestRequest request = GetRequest(method, resource, parameters, body);
            request.RequestFormat = (DataFormat)format;
            
            return restClient.Execute<T>(request).Data;
        }

        public virtual IRestResponse ExecuteRAW(RequestMethod method, string resource, object parameters, RestObject body = null) {
            RestRequest request = GetRequest(method, resource, parameters, body);

            return restClient.Execute(request);
        }

        public T ExecuteGET<T>(string resource, object parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.GET, resource, parameters);
        }

        public T ExecutePOST<T>(string resource, object parameters, T body = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.POST, resource, parameters, body);
        }

        public T ExecutePUT<T>(string resource, object parameters, T body = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.PUT, resource, parameters, body);
        }

        public T ExecuteDELETE<T>(string resource, object parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.DELETE, resource, parameters);
        }

        public T ExecuteOPTIONS<T>(string resource, object parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.OPTIONS, resource, parameters);
        }

        public T ExecutePATCH<T>(string resource, object parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.PATCH, resource, parameters);
        }

        public T ExecuteMERGE<T>(string resource, object parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.MERGE, resource, parameters);
        }

        public T ExecuteHEAD<T>(string resource, object parameters = null) where T : RestObject, new() {
            return Execute<T>(RequestMethod.HEAD, resource, parameters);
        }
    }
}
