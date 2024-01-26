using ContainersMauiApp.ViewModels;

namespace ContainersMauiApp;

public partial class ContainersPage : ContentPage
{
	public ContainersPage(ContainerCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}