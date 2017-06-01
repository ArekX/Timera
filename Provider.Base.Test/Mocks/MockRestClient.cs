using Provider.Base.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Provider.Base.REST.Enums;

namespace Provider.Base.Test.Mocks
{
    class MockRestClient : BaseRestClient
    {
        public MockRestClient() : base("", RestFormat.JSON) {
        }
    }
}
