using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi01.Models;

namespace TestApi01.Repository
{
    public interface IProductosEnMemoria
    {
        IEnumerable<Producto> GetProductos();
        Producto GetProducto(int id);
    }
}
