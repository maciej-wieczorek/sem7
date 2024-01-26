using ContainersMauiApp.ViewModels;

namespace ContainersMauiApp;

public partial class ProducersPage : ContentPage
{
	public ProducersPage(ProducerCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}