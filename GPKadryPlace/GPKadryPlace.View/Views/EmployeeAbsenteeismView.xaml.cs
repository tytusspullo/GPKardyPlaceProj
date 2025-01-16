using GPKadryPlace.Model;
using GPKadryPlace.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace GPKadryPlace.View.Views
{
    /// <summary>
    /// Interaction logic for EmployeeAbsenteeismView.xaml
    /// </summary>
    public partial class EmployeeAbsenteeismView : UserControl
    {
        public EmployeeAbsenteeismView(EmployeeAbsenteeismViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        //public EmployeeAbsenteeismView(EmployeeAbsenteeismViewModel viewModel)
        //{
        //    if (viewModel is null)
        //    {
        //    }
        //    else
        //    {
        //        this.DataContext = viewModel;
        //    }
        //    InitializeComponent();
        ///*
        // * https://www.youtube.com/watch?v=nnxCO4JX1Wc
        // */
        //}
    }
}
