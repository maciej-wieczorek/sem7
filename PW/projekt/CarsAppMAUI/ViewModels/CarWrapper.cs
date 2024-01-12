using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAppMAUI.ViewModels
{
    interface ITest1
    {
        string Name { get; set; }
    }

    public class Test:ITest1
    {
        public string Name { get; set; }
    }

    internal class CarWrapper
    {
    }
}
