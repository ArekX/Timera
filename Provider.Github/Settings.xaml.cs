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

namespace Provider.Github
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Settings : UserControl
    {
        protected Provider provider;

        public Settings(Provider provider)
        {
            InitializeComponent();

            this.provider = provider;
            this.githubToken.Text = provider.Settings.ApiKey;
        }

        private void TestTokenButton_Click(object sender, RoutedEventArgs e) {
            RestClient rest = new RestClient(githubToken.Text);

            provider.Settings.ApiKey = githubToken.Text;

            MessageBox.Show(rest.GetUser().ToString());
        }
    }
}
