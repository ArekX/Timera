﻿using Provider.Base.Storeable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Reflection;

namespace Provider.Github
{
    [Serializable()]
    public class ProviderSettings : BaseSettings
    {
        public ProviderSettings() : base(null, new StreamingContext()) { }

        public ProviderSettings(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        [StoreableSetting(AsName = "api-key")]
        public string ApiKey { get; set; }

        public override ContainerSerializationBinder GetSerializationBinder() {
            return new ContainerSerializationBinder(Assembly.GetExecutingAssembly());
        }
    }
}
