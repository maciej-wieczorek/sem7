using ContainersMauiApp.ViewModels;

namespace ContainersMauiApp;

public partial class ContainerEditPage : ContentPage
{
	public ContainerEditPage(ContainerCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
