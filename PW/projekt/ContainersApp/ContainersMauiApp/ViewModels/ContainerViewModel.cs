using ContainersApp.Core;
using ContainersApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IContainer = ContainersApp.Interfaces.IContainer;
using System.Windows.Input;

namespace ContainersMauiApp.ViewModels
{
    public partial class ContainerViewModel : ObservableValidator, IContainer
    {

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private int id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(2)]
        private string? name;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [Range(1900, 2024)]
        private int productionYear;

        [ObservableProperty]
        private IProducer producer;

        [ObservableProperty]
        private ContainerType type;


        public ContainerViewModel(IContainer container)
        {
            id = container.Id;
            name = container.Name;
            productionYear = container.ProductionYear;
            producer = container.Producer;
            type = container.Type;
        }

        ~ContainerViewModel()
        {
        }

        public ContainerViewModel()
        {
            type = ContainerType.Classic;
        }
    }
}