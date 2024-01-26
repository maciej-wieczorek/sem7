using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
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
                    ProducerEdit.PropertyChanged -= OnPersonEditPropertyChanged;
                    ProducerEdit = null;
                    IsEditing = false;
                    RefreshCanExecute();
                },
                canExecute: () =>
                {
                    return IsEditing;
                });
        }

        [ObservableProperty]
        private ProducerViewModel producerEdit;

        [ObservableProperty]
        private bool isEditing;

        [RelayCommand(CanExecute = nameof(CanCreateNewProducer))]
        private void CreateNewProducer()
        {
            ProducerEdit = new ProducerViewModel();
            ProducerEdit.PropertyChanged += OnPersonEditPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
        }

        private bool CanCreateNewProducer()
        {
            return !IsEditing;
        }

        [RelayCommand(CanExecute = nameof(CanEditProducerBeSaved))]
        private void SaveProducer()
        {
            Producers.Add(producerEdit);
            ProducerEdit.PropertyChanged -= OnPersonEditPropertyChanged;
            ProducerEdit = null;
            IsEditing = false;
            RefreshCanExecute();
        }

        private bool CanEditProducerBeSaved()
        {
            return this.ProducerEdit != null &&
                   ProducerEdit.Name != null &&
                   ProducerEdit.Name.Length > 1 &&
                   ProducerEdit.Address != null &&
                   ProducerEdit.Address.Length > 1;
        }

        private void RefreshCanExecute()
        {
            //(CreateNewProducerCommand as Command)?.ChangeCanExecute();
            CreateNewProducerCommand.NotifyCanExecuteChanged();
            //(SaveProducerCommand as Command)?.ChangeCanExecute();
            SaveProducerCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command)?.ChangeCanExecute();
        }

        void OnPersonEditPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveProducerCommand.NotifyCanExecuteChanged();
        }

        public ICommand CancelCommand { get; set; }
    }
}