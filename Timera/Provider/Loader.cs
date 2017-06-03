using Provider.Base;
using Provider.Base.Storeable;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Timera.Provider
{
    class Loader
    {
        public static void Initialize(string providerPath) {

            string[] paths = Directory.GetFiles(providerPath, "Provider.*.dll");

            foreach (string path in paths) {
                if (path.EndsWith("Provider.Base.dll")) {
                    continue;
                }

                Assembly a = Assembly.LoadFrom(path);

                Debug.WriteLine("Trying to get:" + a.GetName().Name + ".Provider");

                Type myType = a.GetType(a.GetName().Name + ".Provider");

                if (myType == null) {
                    Debug.WriteLine("- Has no Provider class. Skipping.");
                    continue;
                }

                BaseProvider obj = (BaseProvider)Activator.CreateInstance(myType);

                Debug.WriteLine("Loaded Provider: " + obj.Name);

                obj.SetEncryptionKey("KEY!");

                obj.LoadSettings(@"e:\test-" + a.GetName().Name + ".txt");
                obj.Activate();
                obj.SaveSettings(@"e:\test-" + a.GetName().Name + ".txt");
            }
        }
        
    }
}
