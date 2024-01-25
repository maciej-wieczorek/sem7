using MAUIAPP.ViewModels;
using CarsApp.Interfaces;

namespace MAUIAPP;

public partial class MedicinePage : ContentPage
{
	private MedicineCollectionViewModel _viewModel;

	public MedicinePage(MedicineCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

	public void OnSelectedMedicine(object sender, SelectedItemChangedEventArgs args)
	{
		_viewModel.OnSelectedMedicine(args);
	}

}
