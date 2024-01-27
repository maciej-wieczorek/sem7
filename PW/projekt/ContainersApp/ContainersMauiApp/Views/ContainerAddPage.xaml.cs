using ContainersMauiApp.ViewModels;

namespace ContainersMauiApp;

public partial class ContainerAddPage : ContentPage
{
	public ContainerAddPage(ContainerCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}