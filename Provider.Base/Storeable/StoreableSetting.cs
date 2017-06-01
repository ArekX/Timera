using System;
using System.Runtime.CompilerServices;

namespace Provider.Base.Storeable
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class StoreableSetting : Attribute
    {
        public string AsName { get; set; }
        
    }
}
