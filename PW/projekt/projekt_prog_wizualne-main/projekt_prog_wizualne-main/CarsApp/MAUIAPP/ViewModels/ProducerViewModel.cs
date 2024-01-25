using System;
using CarsApp.Core;
using CarsApp.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUIAPP.ViewModels
{
    public partial class ProducerViewModel : ObservableObject, IProducer
    {
        [ObservableProperty]
        public int iD;

        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string address;

        public ProducerViewModel(IProducer producer)
        {
            ID = producer.ID;
            Name = producer.Name;
            Address = producer.Address;
        }

        public ProducerViewModel()
        {

        }
    }
}

