using Provider.Base.Storeable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Github
{
    public class ProviderSettings : BaseSettings
    {
        [StoreableSetting(AsName = "api-key")]
        public string ApiKey { get; set; }
    }
}
