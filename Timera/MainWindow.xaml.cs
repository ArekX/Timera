using Provider.Base;
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
using Timera.Provider;

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

            Loader.Initialize(AppDomain.CurrentDomain.BaseDirectory + @"\Providers");



            foreach(BaseProvider provider in Loader.Providers) {
                TextBlock tb = new TextBlock() {
                    Text = provider.Name
                };

                CheckBox checkBox = new CheckBox();

                checkBox.Unchecked += (object sender, RoutedEventArgs e) => {
                    provider.Deactivate();
                };

                checkBox.Checked += (object sender, RoutedEventArgs e) => {
                    provider.Activate();
                };

                Grid.SetColumn(checkBox, 0);
                Grid.SetColumn(tb, 1);

                Grid grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20) });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });

                grid.Children.Add(tb);
                grid.Children.Add(checkBox);
                
                providerSettings.Items.Add(new TabItem() {
                    Header = grid,
                    Content = new ScrollViewer() {
                        Content = provider.GetSettingsControl()
                    }
                });
            }
        }
    }
}
