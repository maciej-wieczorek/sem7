using ContainersMauiApp.ViewModels;

namespace ContainersMauiApp;

public partial class ContainersPage : ContentPage
{
	private readonly ContainerCollectionViewModel _viewModel;
	public ContainersPage(ContainerCollectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

    public void OnSelectedContainer(object sender, ItemTappedEventArgs args)
	{
		_viewModel.OnSelectedContainer(args);
	}

}