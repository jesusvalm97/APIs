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
        Producto GetProducto(string SKU);
        void CrearProducto(Producto producto);
        void ModificarProducto(Producto producto);
        void BorrarProducto(string SKU);

    }
}
