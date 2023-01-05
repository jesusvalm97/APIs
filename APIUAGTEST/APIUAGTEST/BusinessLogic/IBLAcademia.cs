using APIUAGTEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIUAGTEST.BusinessLogic
{
    public interface IBLAcademia
    {
        IEnumerable<Clase> GetClases();
    }
}
