using CarsApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CarsApp.DAOMock2;

public class DAOMock : DbContext, IDAO
{
    //private List<IProducer> producers;
    //private List<IMedicine> medicines;
    private DbSet<IProducer> producers { get; set; }
    private DbSet<IMedicine> medicines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        string dbPath = @"Medicines.db";
        optionsBuilder.UseSqlite($"Filename={dbPath}");
    }

    public IMedicine CreateNewMedicine()
    {
        return new BO.Medicine();
    }

    public IProducer CreateNewProducer()
    {
        return new BO.Producer();
    }

    public IEnumerable<IMedicine> GetAllMedicines()
    {
        return medicines.ToList();
    }

    public IEnumerable<IProducer> GetAllProducers()
    {
        return producers.ToList();
    }

    public void SaveMedicine(IMedicine med)
    {
        throw new NotImplementedException();
    }

    public void RemoveMedicine(IMedicine med)
    {
        throw new NotImplementedException();
    }

    public void SaveProducer(IProducer prod)
    {
        throw new NotImplementedException();
    }

    public void RemoveProducer(IProducer prod)
    {
        throw new NotImplementedException();
    }
}

