using ContainersMauiApp.ViewModels;

namespace ContainersMauiApp;

public partial class ProducerEditPage : ContentPage
{
	public ProducerEditPage(ProducerCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}