using Provider.Base.REST;
using Provider.Base.REST.Enums;
using System.Collections;
using System.Text;

namespace Provider.Base.Test.Mocks
{
    class MockRestClient : BaseRestClient
    {
        Hashtable mappedObjectResponses;
        Hashtable mappedRawResponses;

        public MockRestClient() : base("", RestFormat.JSON) {
            mappedObjectResponses = new Hashtable();
            mappedRawResponses = new Hashtable();
        }

        public void MapRequestToResponseObject(RequestMethod method, string resource, RestParams paramList, RestObject body, RestObject toResponse) {
            mappedObjectResponses.Add(GetRequestKey(method, resource, paramList, body), toResponse);
        }

        public void MapRequestToResponse(RequestMethod method, string resource, RestParams paramList, RestObject body, MockRestResponse toResponse) {
            mappedObjectResponses.Add(GetRequestKey(method, resource, paramList, body), toResponse);
        }

        public override T Execute<T>(RequestMethod method, string resource, RestParams parameters = null, RestObject body = null) {
            string requestKey = GetRequestKey(method, resource, parameters, body);
            if (mappedObjectResponses.ContainsKey(requestKey)) {
                return (T)mappedObjectResponses[requestKey];
            }

            return null;
        }

        public override RestResponse ExecuteRAW(RequestMethod method, string resource, RestParams parameters, RestObject body = null) {
            string requestKey = GetRequestKey(method, resource, parameters, body);
            if (mappedObjectResponses.ContainsKey(requestKey)) {
                return (MockRestResponse)mappedObjectResponses[requestKey];
            }

            return null;
        }

        protected string GetRequestKey(RequestMethod method, string resource, RestParams paramList, RestObject body) {
            StringBuilder key = new StringBuilder();

            key.Append(method);
            key.Append(resource);
            key.Append(paramList != null ? paramList.GetUniqueId() : "NULL");
            key.Append(body != null ? body.GetUniqueId() : "NULL");

            return key.ToString();
        }
    }
}
