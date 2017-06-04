using Provider.Base;
using Provider.Base.Storeable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Timera.Provider
{
    class Loader
    {
        protected static List<BaseProvider> providers;
        protected static List<string> loadedPaths;

        public static void Initialize(string providerPath) {
            
            if (loadedPaths == null) {
                loadedPaths = new List<string>();
            }

            string result = (from path in loadedPaths where path == providerPath select path as string).SingleOrDefault();

            if (result != null) {
                return;
            }

            string[] paths = Directory.GetFiles(providerPath, "Provider.*.dll");

            foreach (string path in paths) {
   
                Assembly a = Assembly.LoadFrom(path);

                Debug.WriteLine("Trying to get:" + a.GetName().Name + ".Provider");

                Type myType = a.GetType(a.GetName().Name + ".Provider");

                if (myType == null) {
                    continue;
                }

                BaseProvider obj = (BaseProvider)Activator.CreateInstance(myType);

                Providers.Add(obj);
            }
        }

        public static List<BaseProvider> Providers {
            get {
                if (providers == null) {
                    providers = new List<BaseProvider>();
                }

                return providers;
            }
        }
        
    }
}
