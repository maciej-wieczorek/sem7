using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarsApp.Core;

namespace CarsApp.Interfaces
{
	public interface IMedicine
	{
		int ID { get; set; }
		string Name { get; set; }
		IProducer Producer { get; set; }
		double Price { get; set; }
		MedicineType MedType { get; set; }
    }
}

