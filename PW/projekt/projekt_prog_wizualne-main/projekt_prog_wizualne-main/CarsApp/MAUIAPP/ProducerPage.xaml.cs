using MAUIAPP.ViewModels;
using CarsApp.Interfaces;

namespace MAUIAPP;

public partial class ProducerPage : ContentPage
{
    private ProducerCollectionViewModel _viewModel;

    public ProducerPage(ProducerCollectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    public void OnSelectedProducer(object sender, SelectedItemChangedEventArgs args)
    {
        _viewModel.OnSelectedProducer(args);
    }

}
