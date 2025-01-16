using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GPKadryPlace.Model;

namespace GPKadryPlace.ViewModel.ViewModels
{
    public class EmployeeAbsenteeismViewModel: BaseViewModel
    {
        private EmployeeAbsenteeismRepository employeeAbsenteeisms = null;

        public EmployeeAbsenteeismViewModel()
        {
            this.employeeAbsenteeisms = new EmployeeAbsenteeismRepository(LibraryConfig.GetConnectionString());
            this.DataGridItems = new List<DataGridItem>();
            this.DataGridItems = GenerateDiplayItems();
        }
        //public EmployeeAbsenteeismViewModel(string connectionString)
        //{
        //    this.employeeAbsenteeisms = new EmployeeAbsenteeismRepository(connectionString);
        //    this.DataGridItems = GenerateDiplayItems();
        //}
        internal IEnumerable<DataGridItem> DataGridItems { get; set; }
        private List<DataGridItem> GenerateDiplayItems()
        {
            List<DataGridItem> itemsList = new List<DataGridItem>();

            foreach (var item in employeeAbsenteeisms)
            {
                itemsList.Add(new DataGridItem()
                                {
                                    IDEmployeeAbsenteeism = item.IDEmployeeAbsenteeism,
                                    ShortName = item.ShortName,
                                    FullName = item.FullName
                                }
                            );
            }

            return itemsList;
        }
    }
}
