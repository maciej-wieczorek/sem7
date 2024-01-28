using ContainersMauiApp.ViewModels;

namespace ContainersMauiApp;

public partial class ProducerAddPage : ContentPage
{
	public ProducerAddPage(ProducerCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}