namespace ContainersMauiApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ContainerAddPage), typeof(ContainerAddPage));
            Routing.RegisterRoute(nameof(ContainerEditPage), typeof(ContainerEditPage));
            Routing.RegisterRoute(nameof(ProducerAddPage), typeof(ProducerAddPage));
            Routing.RegisterRoute(nameof(ProducerEditPage), typeof(ProducerEditPage));
        }
    }
}
