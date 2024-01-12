using Cars.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarsAppMAUI.ViewModels
{
    public partial class CarCollectionViewModel : ObservableObject
    {
        private IParking ip = new Cars.DB_1.Parking();

        [ObservableProperty]
        private ObservableCollection<ICar> cars;

        public CarCollectionViewModel()
        {
            cars = new ObservableCollection<ICar>();
            foreach (ICar car in ip.GetAllCars())
            {
                cars.Add(new CarViewModel(car));
            }

            CancelCommand = new Command(
                execute: () =>
                {
                    CarEdit.PropertyChanged -= OnPersonEditPropertyChanged;
                    CarEdit = null;
                    IsEditing = false;
                    RefreshCanExecute();
                },
                canExecute: () =>
                {
                    return IsEditing;
                });
        }

        [ObservableProperty]
        private CarViewModel carEdit;

        [ObservableProperty]
        private bool isEditing;

        [RelayCommand(CanExecute = nameof(CanCreateNewCar))]
        private void CreateNewCar()
        {
            CarEdit = new CarViewModel();
            CarEdit.PropertyChanged += OnPersonEditPropertyChanged;
            IsEditing = true;
            RefreshCanExecute();
        }

        private bool CanCreateNewCar()
        {  
            return !IsEditing; 
        }

        [RelayCommand(CanExecute = nameof(CanEditCarBeSaved))]
        private void SaveCar()
        {
            Cars.Add(carEdit);
            CarEdit.PropertyChanged -= OnPersonEditPropertyChanged;
            CarEdit = null;
            IsEditing = false;
            RefreshCanExecute();
        }

        private bool CanEditCarBeSaved()
        {
            return this.CarEdit != null &&
                   CarEdit.Name != null &&
                   CarEdit.Name.Length > 1 &&
                   CarEdit.ProdYear > 1900;
        }

        private void RefreshCanExecute()
        {
            //(CreateNewCarCommand as Command)?.ChangeCanExecute();
            CreateNewCarCommand.NotifyCanExecuteChanged();
            //(SaveCarCommand as Command)?.ChangeCanExecute();
            SaveCarCommand.NotifyCanExecuteChanged();
            (CancelCommand as Command)?.ChangeCanExecute();
        }

        void OnPersonEditPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            SaveCarCommand.NotifyCanExecuteChanged();
        }

        public ICommand CancelCommand { get; set; }

       
    }
}
