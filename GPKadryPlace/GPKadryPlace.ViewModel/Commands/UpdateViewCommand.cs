using GPKadryPlace.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GPKadryPlace.ViewModel.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;
        private string connectionString = string.Empty;

        public UpdateViewCommand(MainViewModel viewModel,string connectionString)
        {
            this.viewModel = viewModel;
            this.connectionString = connectionString;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Home")
            {
                viewModel.SelectedViewModel = new HomeViewModel(this.connectionString);
            }
            else if (parameter.ToString() == "Employee")
            {
                viewModel.SelectedViewModel = new EmployeeViewModel(this.connectionString);
            }
            else if (parameter.ToString() == "EmployeeAbsenteeism")
            { 
                viewModel.SelectedViewModel = new EmployeeAbsenteeismViewModel();
            }
        }
    }
}
