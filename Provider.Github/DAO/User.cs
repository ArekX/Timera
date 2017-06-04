using Provider.Base;
using Provider.Base.Helpers;
using Provider.Base.REST;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Github.DAO
{
    public class User : RestObject
    {
        [DeserializeAs(Name = "id")]
        public int Id { get; set; }

        [DeserializeAs(Name = "login")]
        public string LoginName { get; set; }

        [DeserializeAs(Name = "url")]
        public string ProfileUrl { get; set; }

        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "email")]
        public int Email { get; set; }

        public override string GetUniqueId() {
            return HashHelper.GetSHA256(ToString());
        }

        public override string ToString() {
            return String.Format("[ID = {0}, Name = {1}, Login = {2}, Profile = {3}, Email = {4}",
                Id,
                Name,
                LoginName,
                ProfileUrl,
                Email
            );
        }
    }
}
