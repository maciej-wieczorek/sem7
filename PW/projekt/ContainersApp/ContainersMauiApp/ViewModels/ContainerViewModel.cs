using ContainersApp.Core;
using ContainersApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using IContainer = ContainersApp.Interfaces.IContainer;

namespace ContainersAppMAUI.ViewModels
{
    public partial class ContainerViewModel : ObservableValidator, IContainer
    {

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Id musi byæ nadane")]
        private int id;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [MinLength(2, ErrorMessage = "Nazwa musi mieæ przynkfj oaih21")]
        private string? name;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Rok produkcji musi byæ ")]
        [Range(1900, 2024)]
        private int prodYear;

        [ObservableProperty]
        private IProducer producer;

        [ObservableProperty]
        private ContainerType type;


        public ContainerViewModel(IContainer container)
        {
            id = container.ID;
            name = container.Name;
            prodYear = container.ProductionYear;
            producer = container.Producer;
            type = container.Type;

            ErrorsChanged += CVM_ErrorsChanged;
            PropertyChanged += CVM_PropertyChanged;
        }

        ~ContainerViewModel()
        {
            ErrorsChanged -= CVM_ErrorsChanged;
            PropertyChanged -= CVM_PropertyChanged;
        }

        private void CarViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<string> AllTransmissions { get; } = Enum.GetNames(typeof(ContainerType));

        public string Errors => string.Join(Environment.NewLine, from ValidationResult e in GetErrors(null) select e.ErrorMessage);

        public ContainerViewModel()
        {
            type = ContainerType.Classic;
        }

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


    public sealed class GreatherThanAttribute : ValidationAttribute
    {
        public string PropertyName { get; }
        public GreatherThanAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            object instance = validationContext.ObjectInstance;
            object otherValue = instance.GetType().GetProperty(PropertyName).GetValue(instance);

            if (((IComparable)value).CompareTo(otherValue) > 0)
            {
                return ValidationResult.Success;
            }



            return new ValidationResult($"nowa wartoœc jest mniejsza niz ta {validationContext.MemberName} vs {PropertyName} ");
        }
    }

}