using MAUIAPP.ViewModels;
using Microsoft.Maui.Controls;
using CarsApp.Interfaces;

namespace MAUIAPP
{
    public partial class AddNewProducerPage : ContentPage
    {
        public AddNewProducerPage(ProducerCollectionViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
