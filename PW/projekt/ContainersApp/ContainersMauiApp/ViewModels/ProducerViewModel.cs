using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IProducer = ContainersApp.Interfaces.IProducer;

namespace ContainersMauiApp.ViewModels
{
    public partial class ProducerViewModel : ObservableValidator, IProducer
    {

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        private int id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(2)]
        [Required]
        private string name;

        [ObservableProperty]
        [Required]
        private string address;


        public ProducerViewModel(IProducer producer)
        {
            id = producer.Id;
            name = producer.Name;
            address = producer.Address;
        }
        public ProducerViewModel()
        {
        }
    }
}