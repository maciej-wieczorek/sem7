using System;
using CarsApp.BLC;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Configuration;

namespace MAUIAPP.ViewModels
{
    public partial class ProducerCollectionViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ProducerViewModel> producers;

        private BLC blc;

        public ProducerCollectionViewModel()
        {
            BLC.LibName = ConfigurationManager.AppSettings["DBLibraryName"];
            blc = BLC.GetBLC();

            producers = new ObservableCollection<ProducerViewModel>();

            foreach (var prod in blc.GetProducers())
            {
                producers.Add(new ProducerViewModel(prod));
            }

            IsEditing = false;
            ProdEdit = null;
            tmpProd = null;

            CancelCommand = new Command(

                execute: () =>
                {
                    ProdEdit.PropertyChanged -= OnProducerEditPropertyChanged;
                    ProdEdit = null;
                    tmpProd = null;
                    IsEditing = false;
                    RefreshCanExecute();
                    Shell.Current.GoToAsync("//ProducerPage");
                },
                canExecute: () =>
                {
                    return IsEditing;
                }
                );
        }

        [ObservableProperty]
        private ProducerViewModel prodEdit;

        [ObservableProperty]
        private bool isEditing;

        private ProducerViewModel tmpProd;

        [RelayCommand(CanExecute = nameof(CanCreateNewProducer))]
        private void CreateNewProducer()
        {
            ProdEdit = new ProducerViewModel();
            ProdEdit.PropertyChanged += OnProducerEditPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
            Shell.Current.GoToAsync("//AddNewProducerPage");
        }

        private bool CanCreateNewProducer()
        {
            return !IsEditing;
        }

        private void RefreshCanExecute()
        {
            CreateNewProducerCommand.NotifyCanExecuteChanged();
            SaveProducerCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command).ChangeCanExecute();
        }

        private bool CanEditProdBeSaved()
        {
            return ProdEdit != null &&
                   ProdEdit.Name != null &&
                   ProdEdit.Name.Length > 0;
        }

        private bool CanTmpProdBeDeleted()
        {
            return tmpProd != null;
        }

        [RelayCommand(CanExecute = nameof(CanEditProdBeSaved))]
        private void SaveProducer()
        {
            Producers.Add(prodEdit);
            blc.SaveProducer(prodEdit);
            ProdEdit.PropertyChanged -= OnProducerEditPropertyChanged;
            ProdEdit = null;
            IsEditing = false;
            RefreshCanExecute();
            Shell.Current.GoToAsync("//ProducerPage");
        }

        public void OnSelectedProducer(SelectedItemChangedEventArgs args)
        {
            tmpProd = args.SelectedItem as ProducerViewModel;
            ProdEdit = new ProducerViewModel(tmpProd);
            ProdEdit.PropertyChanged += OnProducerEditPropertyChanged;
            IsEditing = true;

            RefreshCanExecute();
            Shell.Current.GoToAsync("//EditProducerPage");
        }

        private void OnProducerEditPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveProducerCommand.NotifyCanExecuteChanged();
        }

        [RelayCommand(CanExecute = nameof(CanTmpProdBeDeleted))]
        private void DeleteProducer()
        {
            Producers.Remove(tmpProd);
            blc.RemoveProducer(tmpProd);
            ProdEdit = null;
            tmpProd = null;
            IsEditing = false;
            RefreshCanExecute();
            Shell.Current.GoToAsync("//ProducerPage");
        }

        [RelayCommand(CanExecute = nameof(CanEditProdBeSaved))]
        private void UpdateProducer()
        {
            Producers.Remove(tmpProd);
            blc.RemoveProducer(tmpProd);
            Producers.Add(prodEdit);
            blc.SaveProducer(prodEdit);
            ProdEdit.PropertyChanged -= OnProducerEditPropertyChanged;
            ProdEdit = null;
            IsEditing = false;
            RefreshCanExecute();
            Shell.Current.GoToAsync("//ProducerPage");
        }

        public ICommand CancelCommand { get; set; }

    }
}

