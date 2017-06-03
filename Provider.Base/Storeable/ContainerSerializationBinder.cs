using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Base.Storeable
{
    public class ContainerSerializationBinder : SerializationBinder
    {
        protected Assembly container;

        public ContainerSerializationBinder(Assembly container) {
            this.container = container;
        }

        public override Type BindToType(string assemblyName, string typeName) {
            return container.GetType(typeName);
        }
    }
}
