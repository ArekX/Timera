using RestSharp;
using System.Collections;

namespace Provider.Base
{
    public abstract class RestClient
    {
        protected DataFormat format;
        protected string baseUri;
        protected RestSharp.RestClient restClient;

        public RestClient(string baseUri, DataFormat format) {
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
            if (!(parameters is Hashtable)) {
                return;
            }

            Hashtable paramList = (Hashtable)parameters;

            foreach (string key in paramList.Keys) {
                request.AddParameter(key, paramList[key]);
            }
        }

        public virtual BaseDAO Execute(Method method, string resource, object parameters, BaseDAO body = null) {
            RestRequest request = GetRequest(method, resource, parameters, body);

            IRestResponse<BaseDAO> response = restClient.Execute<BaseDAO>(request);
            return response.Data;
        }

        public virtual IRestResponse ExecuteRAW(Method method, string resource, object parameters, BaseDAO body = null) {
            RestRequest request = GetRequest(method, resource, parameters, body);

            return restClient.Execute(request);
        }

        public T ExecuteGET<T>(string resource, object parameters) where T : BaseDAO {
            return (T)Execute(Method.GET, resource, parameters);
        }

        public T ExecutePOST<T>(string resource, object parameters, BaseDAO body = null) where T : BaseDAO {
            return (T)Execute(Method.POST, resource, parameters, body);
        }

        public T ExecutePUT<T>(string resource, object parameters, BaseDAO body = null) where T : BaseDAO {
            return (T)Execute(Method.PUT, resource, parameters, body);
        }

        public T ExecuteDELETE<T>(string resource, object parameters) where T : BaseDAO {
            return (T)Execute(Method.DELETE, resource, parameters);
        }
    }
}
