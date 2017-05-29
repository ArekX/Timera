using System;
using System.Runtime.CompilerServices;

namespace Provider.Base.Storeable
{
    public class StoreableSetting : Attribute
    {
        protected string name;

        public string Name { get { return name; } }
        

        public StoreableSetting([CallerMemberName] string name = null) {
            this.name = name;
        } 
    }
}
