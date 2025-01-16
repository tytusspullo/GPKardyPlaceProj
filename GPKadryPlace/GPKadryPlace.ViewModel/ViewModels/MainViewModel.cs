using GPKadryPlace.ViewModel.ViewModels;
using GPKadryPlace.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GPKadryPlace.ViewModel.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _selectedViewModel;
        private string connectionString = string.Empty;

        public MainViewModel(string connectionString) 
        { 
            this.connectionString = connectionString;
            UpdateViewCommand = new UpdateViewCommand(this, this.connectionString);
        }
        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this, this.connectionString);
        }
        public BaseViewModel SelectedViewModel
        {
            get { 
                return _selectedViewModel; 
            }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }


    }
}
