using CarsApp.Interfaces;
using System;
using System.Collections.Generic;
namespace CarsApp.DAOMock1;

public class DAOMock : IDAO
{
    private List<IProducer> producers;
    private List<IMedicine> medicines;

    public DAOMock()
    {
        producers = new List<IProducer>()
        {
            new BO.Producer() {ID=1, Name="GSK", Address="Warszawa, ul. Polna 1"},
            new BO.Producer() {ID=2, Name="Polfarma"}
        };

        medicines = new List<IMedicine>()
        {
            new BO.Medicine()
            {
                ID=1,
                Producer=producers[0],
                Name="Rutinoskorbin",
                Price=9.99,
                MedType=Core.MedicineType.Dietary_Supplement
            },
            new BO.Medicine()
            {
                ID=2,
                Producer=producers[1],
                Name="Test",
                Price=120.0,
                MedType=Core.MedicineType.Medical_Device
            },
            new BO.Medicine()
            {
                ID=3,
                Producer=producers[0],
                Name="Clatra",
                Price=59.0,
                MedType=Core.MedicineType.Medicine
            }
        };
    }

    public IMedicine CreateNewMedicine()
    {
        return new BO.Medicine() { Name="Przykład"};
    }

    public IProducer CreateNewProducer()
    {
        return new BO.Producer();
    }

    public IEnumerable<IMedicine> GetAllMedicines()
    {
        return medicines;
    }

    public IEnumerable<IProducer> GetAllProducers()
    {
        return producers;
    }

    public void RemoveMedicine(IMedicine med)
    {
        medicines.Remove(med);
    }

    public void RemoveProducer(IProducer prod)
    {
        producers.Remove(prod);
    }

    public void SaveMedicine(IMedicine med)
    {
        if (med != null)
        {
            medicines.Add(med);
        }
    }

    public void SaveProducer(IProducer prod)
    {
        if (prod != null)
        {
            producers.Add(prod);
        }
    }
}

