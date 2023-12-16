namespace MauiApp1;

public partial class BindingPage : ContentPage
{
	public BindingPage()
	{
		InitializeComponent();
        Binding binding = new Binding();
        binding.Source = slider;
        binding.Path = "Value";
    }
}