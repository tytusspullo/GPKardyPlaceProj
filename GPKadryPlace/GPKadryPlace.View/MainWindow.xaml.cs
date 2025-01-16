using GPKadryPlace.ViewModel.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GPKadryPlace.Utils;

namespace GPKadryPlace.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var configurationReader = new ConfigurationReader();
            var connStr = configurationReader.ReturnConnection();
            GPKadryPlace.View.LibraryConfig.SetConnectionString(connStr);
            GPKadryPlace.ViewModel.LibraryConfig.SetConnectionString(connStr);
            GPKadryPlace.Model.LibraryConfig.SetConnectionString(connStr);

            DataContext = new MainViewModel(connStr);

        }

    }
}