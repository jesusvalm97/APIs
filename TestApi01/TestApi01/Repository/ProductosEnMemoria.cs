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
            new Producto(){ Id = 1, Nombre = "Martillo", Descripcion = "Martillo super preciso", Precio = 12.99, FechaAlta = DateTime.Now },
            new Producto(){ Id = 2, Nombre = "Caja de clavos", Descripcion = "100 unidades de clavos", Precio = 10, FechaAlta = DateTime.Now },
            new Producto(){ Id = 3, Nombre = "Destornillador", Descripcion = "Excelente destornillador", Precio = 9.99, FechaAlta = DateTime.Now },
            new Producto(){ Id = 4, Nombre = "Foco", Descripcion = "Foco chido", Precio = 3, FechaAlta = DateTime.Now }
        };

        public IEnumerable<Producto> GetProductos()
        {
            return productos;
        }

        public Producto GetProducto(int id)
        {
            return productos.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
