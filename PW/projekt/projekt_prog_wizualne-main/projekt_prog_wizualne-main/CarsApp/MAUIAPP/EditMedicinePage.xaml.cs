using MAUIAPP.ViewModels;
using Microsoft.Maui.Controls;
using CarsApp.Interfaces;

namespace MAUIAPP
{
    public partial class EditMedicinePage : ContentPage
    {
        public EditMedicinePage(MedicineCollectionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private async void CancelCommand(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
