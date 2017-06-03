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
using System.Windows.Shapes;

namespace Provider.Github
{
    /// <summary>
    /// Interaction logic for TokenForm.xaml
    /// </summary>
    public partial class TokenForm : Window
    {
        protected Provider provider;

        public TokenForm(Provider provider) {
            InitializeComponent();
            this.provider = provider;
            this.githubToken.Text = provider.Settings.ApiKey;
        }

        private void testTokenButton_Click(object sender, RoutedEventArgs e) {
            RestClient rest = new RestClient(githubToken.Text);

            provider.Settings.ApiKey = githubToken.Text;

            MessageBox.Show(rest.GetUser().ToString());
        }

        private void okButton_Click(object sender, RoutedEventArgs e) {

        }
    }
}
