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
        private string? name;

        [ObservableProperty]
        private string? address;


        public ProducerViewModel(IProducer producer)
        {
            id = producer.Id;
            name = producer.Name;
            address = producer.Address;

            ErrorsChanged += CVM_ErrorsChanged;
            PropertyChanged += CVM_PropertyChanged;
        }
        public ProducerViewModel()
        {
        }

        ~ProducerViewModel()
        {
            ErrorsChanged -= CVM_ErrorsChanged;
            PropertyChanged -= CVM_PropertyChanged;
        }

        private void ProducerViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);


        private void CVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(HasErrors))
            {
                OnPropertyChanged(nameof(HasErrors));
            }
        }

        private void CVM_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Errors));
            //OnPropertyChanged("Item[]");
        }

        public IEnumerable<string> NameErrors
        {
            get { return this["Name"]; }
        }

        // public Dictionary<string,IEnumerable<string>> ErrorsCol

        // [IndexerName("Item")]
        public IEnumerable<string> this[string propertyName]
        {
            get { return from ValidationResult e in GetErrors(propertyName) select e.ErrorMessage; }

        }
    }
}