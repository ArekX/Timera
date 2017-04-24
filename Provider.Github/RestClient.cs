
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Provider.Base;
using RestSharp;
using Provider.Github.DAO;

namespace Provider.Github
{
    class RestClient : BaseRestClient
    {
        public string token;

        public RestClient(string token) : base("https://api.github.com", DataFormat.Json) {
            this.token = token;
        }

        protected override RestRequest GetRequest(Method method, string resource, object parameters, BaseDAO body = null) {
            RestRequest request = base.GetRequest(method, resource, parameters, body);

            request.AddHeader("Authorization", "Bearer " + token);

            return request;
        }

        public User GetUser() {
            return ExecuteGET<User>("/user");
        }
    }
}
