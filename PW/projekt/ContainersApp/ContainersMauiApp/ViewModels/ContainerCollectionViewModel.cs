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
                    ContainerEdit.PropertyChanged -= OnContainerEditPropertyChanged;
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
        private bool CanCreateNewContainer()
        {
            return !IsEditing;
        }

        [RelayCommand(CanExecute = nameof(CanCreateNewContainer))]
        private void CreateNewContainer()
        {
            ContainerEdit = new ContainerViewModel();
            _blc.AddContainer(containerEdit);
            Containers.Add(containerEdit);
            ContainerEdit.PropertyChanged += OnContainerEditPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
            Shell.Current.GoToAsync(nameof(ContainerAddPage));
        }

        [RelayCommand(CanExecute = nameof(CanEditContainerBeSaved))]
        private void SaveContainer()
        {
            _blc.AddContainer(containerEdit);
            Containers.Add(containerEdit);
            ContainerEdit.PropertyChanged -= OnContainerEditPropertyChanged;
            ContainerEdit = null;
            IsEditing = false;
            RefreshCanExecute();
        }

        [RelayCommand]
        private void DeleteContainer()
        {
            _blc.DeleteContainer(containerEdit.Id);
            Containers.Remove(containerEdit);
            ContainerEdit.PropertyChanged -= OnContainerEditPropertyChanged;
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


        public void OnSelectedContainer(SelectedItemChangedEventArgs args)
		{
			ContainerEdit = new ContainerViewModel(args.SelectedItem as ContainerViewModel);
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

        public ICommand CancelCommand { get; set; }
    }
}
