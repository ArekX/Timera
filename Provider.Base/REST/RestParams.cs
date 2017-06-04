using Provider.Base.Helpers;
using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Provider.Base.REST
{
    public class RestParams
    {
        protected Hashtable map;
        public RestParams() {
            map = new Hashtable();
        }

        public static RestParams Make() {
            return new RestParams();
        }

        public RestParams Set(string param, object value) {
            map.Add(param, value);
            return this;
        }

        public object Get(string param, object defaultValue = null) {
            if (!map.ContainsKey(param)) {
                return defaultValue;
            }

            return map[param];
        }

        public RestParams Remove(string param) {
            map.Remove(param);
            return this;
        }

        public Hashtable GetMap() {
            return map;
        }

        public string GetUniqueId() {
            StringBuilder uniqueId = new StringBuilder();
            foreach(String param in map.Keys) {
                uniqueId.Append(param).Append("-").Append(map[param]);
            }

            return HashHelper.GetSHA256(uniqueId.ToString());
        }
    }
}
