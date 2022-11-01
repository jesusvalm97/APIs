using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi01.Models;

namespace TestApi01.Repository
{
    public interface IProductosEnMemoria
    {
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<Producto> GetProductoAsync(string SKU);
        Task CrearProductoAsync(Producto producto);
        Task ModificarProductoAsync(Producto producto);
        Task BorrarProductoAsync(string SKU);

    }
}
