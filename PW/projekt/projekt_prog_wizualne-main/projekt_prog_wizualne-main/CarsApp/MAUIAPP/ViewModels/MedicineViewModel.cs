using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using CarsApp.Core;
using CarsApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUIAPP.ViewModels
{
	public partial class MedicineViewModel: ObservableObject, IMedicine
	{
        [ObservableProperty]
        private int iD;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private IProducer producer;

        [ObservableProperty]
        private double price;

        [ObservableProperty]
        private MedicineType medType;

        public MedicineViewModel( IMedicine medicine )
        {
            ID = medicine.ID;
            Name = medicine.Name;
            Producer = medicine.Producer;
            Price = medicine.Price;
            MedType = medicine.MedType;
        }

        public MedicineViewModel()
        {

        }
    }
}

