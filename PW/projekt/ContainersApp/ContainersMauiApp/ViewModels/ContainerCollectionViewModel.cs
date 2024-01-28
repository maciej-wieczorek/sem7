using ContainersApp.Core;
using ContainersApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Globalization;
using ContainersApp.BLC;
using IContainer = ContainersApp.Interfaces.IContainer;

namespace ContainersMauiApp.ViewModels
{
    public partial class ContainerCollectionViewModel : ObservableObject, INotifyPropertyChanged
    {
        private readonly BLC _blc;

        [ObservableProperty]
        private ObservableCollection<IContainer> containers;


        public ContainerCollectionViewModel(BLC blc)
        {
            _blc = blc;
            containers = new ObservableCollection<IContainer>();
            foreach (IContainer container in _blc.GetAllContainers())
            {
                containers.Add(new ContainerViewModel(container));
            }

            CancelCommand = new Command(
                execute: () =>
                {
                    ContainerEdit.PropertyChanged -= OnContainerEditPropertyChanged;
                    ContainerEdit = null;
                    IsEditing = false;
                    IsAdding = false;
                    RefreshCanExecute();
                    Shell.Current.GoToAsync("..");
                },
                canExecute: () =>
                {
                    return IsEditing || IsAdding;
                });
        }
        public List<IProducer> ProducersList
        { 
            get
            {
                return _blc.GetAllProducers().ToList();
            }
        }
        public List<string> ContainerTypes
        {
            get
            {
                return Enum.GetNames(typeof(ContainerType)).ToList();
            }
        }

        [ObservableProperty]
        private ContainerViewModel containerEdit;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private bool isAdding;
        private bool CanCreateNewContainer()
        {
            return !IsAdding;
        }

        [RelayCommand(CanExecute = nameof(CanCreateNewContainer))]
        private void CreateNewContainer()
        {
            OnPropertyChanged(nameof(ProducersList));
            ContainerEdit = new ContainerViewModel();
            ContainerEdit.PropertyChanged += OnContainerEditPropertyChanged;
            IsAdding = true;
            RefreshCanExecute();
            Shell.Current.GoToAsync(nameof(ContainerAddPage));
        }

        [RelayCommand(CanExecute = nameof(CanEditContainerBeSaved))]
        private void SaveContainer()
        {
            if (IsAdding)
            {
                _blc.AddContainer(ContainerEdit);
                Containers.Add(ContainerEdit);
                IsAdding = false;
            }
            else if (IsEditing)
            {
                _blc.UpdateContainer(ContainerEdit);
                IsEditing = false;
            }
            ContainerEdit.PropertyChanged -= OnContainerEditPropertyChanged;
            ContainerEdit = null;
            RefreshCanExecute();
            Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void DeleteContainer()
        {
            IContainer itemToRemove = Containers.FirstOrDefault(c => c.Id == ContainerEdit.Id);
            if (itemToRemove != null)
            {
                _blc.DeleteContainer(ContainerEdit.Id);
                Containers.Remove(itemToRemove);
            }
            ContainerEdit.PropertyChanged -= OnContainerEditPropertyChanged;
            ContainerEdit = null;
            IsEditing = false;
            RefreshCanExecute();
            Shell.Current.GoToAsync("..");
        }

        private bool CanEditContainerBeSaved()
        {
            return ContainerEdit != null &&
                   ContainerEdit.Name != null &&
                   ContainerEdit.Name.Length > 1 &&
                   ContainerEdit.ProductionYear > 1900;
        }


        public void OnSelectedContainer(ItemTappedEventArgs args)
		{
            OnPropertyChanged(nameof(ProducersList));
			ContainerEdit = args.Item as ContainerViewModel;
            ContainerEdit.PropertyChanged += OnContainerEditPropertyChanged;
			IsEditing = true;
			
			RefreshCanExecute();
			Shell.Current.GoToAsync(nameof(ContainerEditPage));
		}


        private void RefreshCanExecute()
        {
            //(CreateNewContainerCommand as Command)?.ChangeCanExecute();
            CreateNewContainerCommand.NotifyCanExecuteChanged();
            //(SaveContainerCommand as Command)?.ChangeCanExecute();
            SaveContainerCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command)?.ChangeCanExecute();
        }

        void OnContainerEditPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveContainerCommand.NotifyCanExecuteChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand CancelCommand { get; set; }
    }

    public class ContainerTypeToIntConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            ContainerType containerType = (ContainerType)value;
            int result = (int)containerType;
            return result;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            int val = (int)value;
            if (val != -1)
                return (ContainerType)value;
            else
                return 0;
        }
    }

}
