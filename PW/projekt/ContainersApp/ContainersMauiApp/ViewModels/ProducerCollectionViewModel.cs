using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Configuration;
using ContainersApp.BLC;
using IProducer = ContainersApp.Interfaces.IProducer;

namespace ContainersMauiApp.ViewModels
{
    public partial class ProducerCollectionViewModel : ObservableObject
    {
        private readonly BLC _blc;

        [ObservableProperty]
        private ObservableCollection<IProducer> producers;

        public ProducerCollectionViewModel(BLC blc)
        {
            _blc = blc;
            producers = new ObservableCollection<IProducer>();
            foreach (IProducer producer in _blc.GetAllProducers())
            {
                producers.Add(new ProducerViewModel(producer));
            }

            CancelCommand = new Command(
                execute: () =>
                {
                    ProducerEdit.PropertyChanged -= OnProducerEditPropertyChanged;
                    ProducerEdit = null;
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

        [ObservableProperty]
        private ProducerViewModel producerEdit;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private bool isAdding;

        [RelayCommand(CanExecute = nameof(CanCreateNewProducer))]
        private void CreateNewProducer()
        {
            ProducerEdit = new ProducerViewModel();
            ProducerEdit.PropertyChanged += OnProducerEditPropertyChanged;
            IsAdding = true;
            RefreshCanExecute();
            Shell.Current.GoToAsync(nameof(ProducerAddPage));
        }

        private bool CanCreateNewProducer()
        {
            return !IsAdding;
        }

        [RelayCommand(CanExecute = nameof(CanEditProducerBeSaved))]
        private void SaveProducer()
        {
            if (IsAdding)
            {
                _blc.AddProducer(ProducerEdit);
                Producers.Add(ProducerEdit);
                IsAdding = false;
            }
            else if (IsEditing)
            {
                _blc.UpdateProducer(ProducerEdit);
                IsEditing = false;
            }
            ProducerEdit.PropertyChanged -= OnProducerEditPropertyChanged;
            ProducerEdit = null;
            RefreshCanExecute();
            Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void DeleteProducer()
        {
            IProducer itemToRemove = Producers.FirstOrDefault(c => c.Id == ProducerEdit.Id);
            if (itemToRemove != null)
            {
                _blc.DeleteProducer(ProducerEdit.Id);
                Producers.Remove(itemToRemove);
            }
            ProducerEdit.PropertyChanged -= OnProducerEditPropertyChanged;
            ProducerEdit = null;
            IsEditing = false;
            RefreshCanExecute();
            Shell.Current.GoToAsync("..");
        }

        private bool CanEditProducerBeSaved()
        {
            return this.ProducerEdit != null &&
                   ProducerEdit.Name != null &&
                   ProducerEdit.Name.Length > 1 &&
                   ProducerEdit.Address != null &&
                   ProducerEdit.Address.Length > 1;
        }

        public void OnSelectedProducer(ItemTappedEventArgs args)
		{
			ProducerEdit = args.Item as ProducerViewModel;
            ProducerEdit.PropertyChanged += OnProducerEditPropertyChanged;
			IsEditing = true;
			
			RefreshCanExecute();
			Shell.Current.GoToAsync(nameof(ProducerEditPage));
		}

        private void RefreshCanExecute()
        {
            //(CreateNewProducerCommand as Command)?.ChangeCanExecute();
            CreateNewProducerCommand.NotifyCanExecuteChanged();
            //(SaveProducerCommand as Command)?.ChangeCanExecute();
            SaveProducerCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command)?.ChangeCanExecute();
        }

        void OnProducerEditPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveProducerCommand.NotifyCanExecuteChanged();
        }

        public ICommand CancelCommand { get; set; }
    }
}