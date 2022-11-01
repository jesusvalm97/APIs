using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi01.Models;

namespace TestApi01.Repository
{
    public class ProductosEnMemoria : IProductosEnMemoria
    {
        private readonly List<Producto> productos = new List<Producto>() {
            new Producto(){ Id = 1, Nombre = "Martillo", Descripcion = "Martillo super preciso", Precio = 12.99, FechaAlta = DateTime.Now, SKU = "MART01" },
            new Producto(){ Id = 2, Nombre = "Caja de clavos", Descripcion = "100 unidades de clavos", Precio = 10, FechaAlta = DateTime.Now, SKU = "CLAV01" },
            new Producto(){ Id = 3, Nombre = "Destornillador", Descripcion = "Excelente destornillador", Precio = 9.99, FechaAlta = DateTime.Now, SKU = "DEST01" },
            new Producto(){ Id = 4, Nombre = "Foco", Descripcion = "Foco chido", Precio = 3, FechaAlta = DateTime.Now, SKU = "FOC01" }
        };

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            return await Task.FromResult(productos);
        }

        public async Task<Producto> GetProductoAsync(string SKU)
        {
            return await Task.FromResult(productos.Where(p => p.SKU == SKU).FirstOrDefault());
        }

        public async Task CrearProductoAsync(Producto producto)
        {
            productos.Add(producto);
            await Task.CompletedTask;
        }

        public async Task ModificarProductoAsync(Producto producto)
        {
            int index = productos.FindIndex(p => p.Id == producto.Id);
            productos[index] = producto;
            await Task.CompletedTask;
        }

        public async Task BorrarProductoAsync(string SKU)
        {
            int index = productos.FindIndex(p => p.SKU == SKU);
            productos.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}
