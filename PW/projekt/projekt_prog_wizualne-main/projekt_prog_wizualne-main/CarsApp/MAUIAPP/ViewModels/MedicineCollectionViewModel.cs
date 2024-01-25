using System;
using System.Collections.ObjectModel;
using CarsApp.BLC;
using CarsApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Configuration;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows.Input;
using System.Globalization;
using CarsApp.Core;

namespace MAUIAPP.ViewModels
{
	public partial class MedicineCollectionViewModel: ObservableObject
	{
		[ObservableProperty]
		private ObservableCollection<MedicineViewModel> medicines;

		private BLC blc;

		public MedicineCollectionViewModel()
		{
			BLC.LibName = ConfigurationManager.AppSettings["DBLibraryName"];
			blc = BLC.GetBLC();

			medicines = new ObservableCollection<MedicineViewModel>();

			foreach (var med in blc.GetMedicines())
			{
				medicines.Add(new MedicineViewModel(med));
			}

            MedicineTypeList = new List<string>(Array.ConvertAll(medicineTypes, x => x.ToString()));

			IsEditing = false;
			MedEdit = null;
			tmpMed = null;

			CancelCommand = new Command(

				execute: () =>
				{
					MedEdit.PropertyChanged -= OnMedicineEditPropertyChanged;
					MedEdit = null;
					tmpMed = null;
					IsEditing = false;
					RefreshCanExecute();
					Shell.Current.GoToAsync("//MedicinePage");
				},
				canExecute: () =>
				{
					return IsEditing;
				}
				);
		}

		[ObservableProperty]
		private MedicineViewModel medEdit;

		[ObservableProperty]
		private bool isEditing;

		private MedicineViewModel tmpMed;

        public static MedicineType[] medicineTypes = (MedicineType[])Enum.GetValues(typeof(MedicineType));

		[ObservableProperty]
		public List<string> medicineTypeList;

        [RelayCommand(CanExecute = nameof(CanCreateNewMedicine))]
		private void CreateNewMedicine()
		{
			MedEdit = new MedicineViewModel();
			MedEdit.PropertyChanged += OnMedicineEditPropertyChanged;
			IsEditing = true;
			RefreshCanExecute();
			foreach (var item in blc.GetProducers())
			{
				Console.WriteLine(item.Name);
			}
            Shell.Current.GoToAsync("//AddNewMedicinePage", false);
		}

		private bool CanCreateNewMedicine()
		{
			return !IsEditing;
		}

		private void RefreshCanExecute()
		{
			CreateNewMedicineCommand.NotifyCanExecuteChanged();
			SaveMedicineCommand.NotifyCanExecuteChanged();
			(CancelCommand as Command).ChangeCanExecute();
		}

        private bool CanEditMedBeSaved()
        {
            return MedEdit != null &&
                   MedEdit.Name != null &&
                   MedEdit.Name.Length > 0 &&
                   MedEdit.Price >= 0;
        }

		private bool CanTmpMedBeDeleted()
		{
			return tmpMed != null;
		}

        [RelayCommand(CanExecute =nameof(CanEditMedBeSaved))]
		private void SaveMedicine()
		{
			Medicines.Add(medEdit);
            blc.SaveMedicine(medEdit);
            MedEdit.PropertyChanged -= OnMedicineEditPropertyChanged;
			MedEdit = null;
			IsEditing = false;
			RefreshCanExecute();
			Shell.Current.GoToAsync("//MedicinePage");
		}

		public void OnSelectedMedicine(SelectedItemChangedEventArgs args)
		{
            tmpMed = args.SelectedItem as MedicineViewModel;
			MedEdit = new MedicineViewModel(tmpMed);
            MedEdit.PropertyChanged += OnMedicineEditPropertyChanged;
			IsEditing = true;
			
			RefreshCanExecute();
			Shell.Current.GoToAsync("//EditMedicinePage");
		}

		private void OnMedicineEditPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			SaveMedicineCommand.NotifyCanExecuteChanged();
		}

		[RelayCommand(CanExecute =nameof(CanTmpMedBeDeleted))]
		private void DeleteMedicine()
		{
			Medicines.Remove(tmpMed);
			blc.RemoveMedicine(tmpMed);
			MedEdit = null;
			tmpMed = null;
			IsEditing = false;
			RefreshCanExecute();
			Shell.Current.GoToAsync("//MedicinePage");
		}

		[RelayCommand(CanExecute =nameof(CanEditMedBeSaved))]
		private void UpdateMedicine()
		{
            Medicines.Remove(tmpMed);
            blc.RemoveMedicine(tmpMed);
            Medicines.Add(medEdit);
            blc.SaveMedicine(medEdit);
            MedEdit.PropertyChanged -= OnMedicineEditPropertyChanged;
            MedEdit = null;
            IsEditing = false;
            RefreshCanExecute();
            Shell.Current.GoToAsync("//MedicinePage");
        }

        public ICommand CancelCommand { get; set; }

    }

    public class MyEnumToIntConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            MedicineType transmissionType = (MedicineType)value;
            int result = (int)transmissionType;
            return result;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            int val = (int)value;
            if (val != -1)
                return (MedicineType)value;
            else
                return 0;

        }
    }
}

