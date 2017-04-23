using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using Provider.Base;

namespace Timera
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            string[] paths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + @"\Providers", "Provider.*.dll");

            foreach(string path in paths) {
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

                obj.Activate();
            }
        }
    }
}
