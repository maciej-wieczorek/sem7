using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CarsApp.Interfaces
{
	public interface IDAO
	{
		IEnumerable<IProducer> GetAllProducers();
		IEnumerable<IMedicine> GetAllMedicines();

		IProducer CreateNewProducer();
		IMedicine CreateNewMedicine();

		void SaveMedicine(IMedicine med);
		void RemoveMedicine(IMedicine med);

		void SaveProducer(IProducer prod);
		void RemoveProducer(IProducer prod);
	}
}

