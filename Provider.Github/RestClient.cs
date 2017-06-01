using RestSharp;
using Provider.Github.DAO;
using Provider.Base.REST;
using Provider.Base.REST.Enums;

namespace Provider.Github
{
    class RestClient : BaseRestClient
    {
        public string token;

        public RestClient(string token) : base("https://api.github.com", RestFormat.JSON) {
            this.token = token;
        }

        protected override RestRequest GetRequest(RequestMethod method, string resource, object parameters, RestObject body = null) {
            RestRequest request = base.GetRequest(method, resource, parameters, body);

            request.AddHeader("Authorization", "Bearer " + token);

            return request;
        }

        public User GetUser() {
            return ExecuteGET<User>("/user");
        }
    }
}
