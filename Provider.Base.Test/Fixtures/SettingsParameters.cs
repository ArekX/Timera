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
    public class SettingsParameters : BaseSettings
    {
        public SettingsParameters(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        public SettingsParameters() : base(null, new StreamingContext()) { }

        public override ContainerSerializationBinder GetSerializationBinder() {
            return new ContainerSerializationBinder(Assembly.GetCallingAssembly());
        }

        [StoreableSetting()]
        public string TestString { get; set; }

        [StoreableSetting(AsName = "test-string-as-new-key")]
        public string TestStringAs { get; set; }

        public string NotSerializableParameter { get; set; }
    }
}
