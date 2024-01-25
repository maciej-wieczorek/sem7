using CarsApp.Interfaces;
using System.Reflection;
namespace CarsApp.BLC;

public sealed class BLC
{
    private IDAO dao;

    public static BLC blc = null;

    public static string LibName { get; set; }

    public BLC(string libraryName)
    {
        Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);

        Type? typeToCreate = null;

        foreach(Type t in assembly.GetTypes())
        {
            if (t.IsAssignableTo(typeof(IDAO)))
            {
                typeToCreate = t;
                break;
            }
        }
        dao = (IDAO)Activator.CreateInstance(typeToCreate, null);
    }

    public static BLC GetBLC()
    {
        if (blc == null && !string.IsNullOrEmpty(LibName))
        {
            blc = new BLC(LibName);
        }
        return blc;
    }

    public IEnumerable<IProducer> GetProducers()
        => dao.GetAllProducers();

    public IEnumerable<IMedicine> GetMedicines()
        => dao.GetAllMedicines();

    public void SaveMedicine(IMedicine med)
        => dao.SaveMedicine(med);

    public void RemoveMedicine(IMedicine med)
        => dao.RemoveMedicine(med);

    public void SaveProducer(IProducer prod)
        => dao.SaveProducer(prod);

    public void RemoveProducer(IProducer prod)
        => dao.RemoveProducer(prod);
}

