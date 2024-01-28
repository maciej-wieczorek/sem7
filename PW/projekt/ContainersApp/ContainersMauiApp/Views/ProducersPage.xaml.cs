using ContainersMauiApp.ViewModels;

namespace ContainersMauiApp;

public partial class ProducersPage : ContentPage
{
	private readonly ProducerCollectionViewModel _viewModel;
	public ProducersPage(ProducerCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

    public void OnSelectedProducer(object sender, ItemTappedEventArgs args)
	{
		_viewModel.OnSelectedProducer(args);
	}
}