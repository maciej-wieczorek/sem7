using ContainersApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ContainersApp.BLC;
using IContainer = ContainersApp.Interfaces.IContainer;

namespace ContainersMauiApp.ViewModels
{
    public partial class ContainerCollectionViewModel : ObservableObject
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
                    ContainerEdit.PropertyChanged -= OnPersonEditPropertyChanged;
                    ContainerEdit = null;
                    IsEditing = false;
                    RefreshCanExecute();
                },
                canExecute: () =>
                {
                    return IsEditing;
                });
        }

        [ObservableProperty]
        private ContainerViewModel containerEdit;

        [ObservableProperty]
        private bool isEditing;

        [RelayCommand(CanExecute = nameof(CanCreateNewContainer))]
        private void CreateNewContainer()
        {
            ContainerEdit = new ContainerViewModel();
            ContainerEdit.PropertyChanged += OnPersonEditPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
        }

        private bool CanCreateNewContainer()
        {
            return !IsEditing;
        }

        [RelayCommand(CanExecute = nameof(CanEditContainerBeSaved))]
        private void SaveContainer()
        {
            Containers.Add(containerEdit);
            ContainerEdit.PropertyChanged -= OnPersonEditPropertyChanged;
            ContainerEdit = null;
            IsEditing = false;
            RefreshCanExecute();
        }

        private bool CanEditContainerBeSaved()
        {
            return this.ContainerEdit != null &&
                   ContainerEdit.Name != null &&
                   ContainerEdit.Name.Length > 1 &&
                   ContainerEdit.ProductionYear > 1900;
        }

        private void RefreshCanExecute()
        {
            //(CreateNewContainerCommand as Command)?.ChangeCanExecute();
            CreateNewContainerCommand.NotifyCanExecuteChanged();
            //(SaveContainerCommand as Command)?.ChangeCanExecute();
            SaveContainerCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command)?.ChangeCanExecute();
        }

        void OnPersonEditPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveContainerCommand.NotifyCanExecuteChanged();
        }

        public ICommand CancelCommand { get; set; }
    }
}
