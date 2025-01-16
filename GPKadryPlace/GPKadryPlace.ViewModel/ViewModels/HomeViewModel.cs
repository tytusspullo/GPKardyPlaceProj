using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPKadryPlace.ViewModel.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private string connectionString = string.Empty;
        public HomeViewModel(string connectionString) 
        { 
            this.connectionString = connectionString;
        }
    }
}
