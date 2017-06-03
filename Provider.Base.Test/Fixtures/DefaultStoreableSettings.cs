using Provider.Base.Storeable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace Provider.Base.Test.Fixtures
{
    [Serializable]
    public class DefaultStoreableSettings : BaseSettings
    {
        public DefaultStoreableSettings(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        public DefaultStoreableSettings() : base(null, new StreamingContext()) { }

        public override ContainerSerializationBinder GetSerializationBinder() {
            return new ContainerSerializationBinder(Assembly.GetCallingAssembly());
        }

        [StoreableSetting()]
        public string TestString { get; set; }

        [StoreableSetting(AsName = "test-string-as-new-key")]
        public string TestStringAs { get; set; }


        [StoreableSetting()]
        public int TestRandomInteger { get; set; }
    }
}
