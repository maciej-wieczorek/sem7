using MAUIAPP.ViewModels;
using Microsoft.Maui.Controls;
using CarsApp.Interfaces;

namespace MAUIAPP
{
    public partial class AddNewMedicinePage : ContentPage
    {
        public AddNewMedicinePage(MedicineCollectionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }

}
