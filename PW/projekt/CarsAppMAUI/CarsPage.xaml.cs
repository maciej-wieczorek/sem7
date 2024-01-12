using CarsAppMAUI.ViewModels;

namespace CarsAppMAUI;

public partial class CarsPage : ContentPage
{
	public CarsPage(CarCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}