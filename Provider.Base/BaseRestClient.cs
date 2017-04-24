using RestSharp;
using System;
using System.Collections;
using System.Diagnostics;

namespace Provider.Base
{
    public abstract class BaseRestClient
    {
        protected DataFormat format;
        protected string baseUri;
        protected RestClient restClient;

        public BaseRestClient(string baseUri, DataFormat format) {
            this.baseUri = baseUri;
            this.format = format;

            restClient = new RestSharp.RestClient(baseUri);
        }

        protected virtual RestRequest GetRequest(Method method, string resource, object parameters, BaseDAO body = null) {
            RestRequest request = new RestRequest(resource, method);

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

        public virtual T Execute<T>(Method method, string resource, object parameters = null, BaseDAO body = null) where T : BaseDAO, new() {
            RestRequest request = GetRequest(method, resource, parameters, body);
            request.RequestFormat = this.format;

            Debug.WriteLine("Sending request to: " + baseUri + resource);
            IRestResponse<T> response = restClient.Execute<T>(request);
     
            Debug.WriteLine("Got Content " + response.Content);

            return response.Data;
        }

        public virtual IRestResponse ExecuteRAW(Method method, string resource, object parameters, BaseDAO body = null) {
            RestRequest request = GetRequest(method, resource, parameters, body);

            return restClient.Execute(request);
        }

        public T ExecuteGET<T>(string resource, object parameters = null) where T : BaseDAO, new() {
            return Execute<T>(Method.GET, resource, parameters);
        }

        public T ExecutePOST<T>(string resource, object parameters, T body = null) where T : BaseDAO, new() {
            return Execute<T>(Method.POST, resource, parameters, body);
        }

        public T ExecutePUT<T>(string resource, object parameters, T body = null) where T : BaseDAO, new() {
            return Execute<T>(Method.PUT, resource, parameters, body);
        }

        public T ExecuteDELETE<T>(string resource, object parameters = null) where T : BaseDAO, new() {
            return Execute<T>(Method.DELETE, resource, parameters);
        }
    }
}
