using MAUIAPP.ViewModels;
using Microsoft.Maui.Controls;
using CarsApp.Interfaces;

namespace MAUIAPP
{
    public partial class EditProducerPage : ContentPage
    {
        public EditProducerPage(ProducerCollectionViewModel viewModel)
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
