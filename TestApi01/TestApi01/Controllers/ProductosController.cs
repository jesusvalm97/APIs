using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi01.DTO;
using TestApi01.Models;
using TestApi01.Repository;

namespace TestApi01.Controllers
{
    [Route("productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosEnMemoria repositorio;

        public ProductosController(IProductosEnMemoria r)
        {
            repositorio = r;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductoDTO>> GetProductos()
        {
            var result = (await repositorio.GetProductosAsync()).Select(p => p.ConvertirDTO());
            return result;
        }

        [HttpGet("{codProducto}")]
        public async Task<ActionResult<ProductoDTO>> GetProducto(string codProducto)
        {
            var result = (await repositorio.GetProductoAsync(codProducto)).ConvertirDTO();

            if (result is null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> CrearProducto(ProductoDTO productoDTO)
        {
            Producto producto = new Producto()
            {
                Nombre = productoDTO.Nombre,
                Descripcion = productoDTO.Descripcion,
                Precio = productoDTO.Precio,
                FechaAlta = DateTime.Now,
                SKU = productoDTO.SKU
            };
            await repositorio.CrearProductoAsync(producto);

            return producto.ConvertirDTO();
        }

        [HttpPut]
        public async Task<ActionResult<ProductoDTO>> ModificarProducto(string codProducto, ProductoDTO productoDTO)
        {
            Producto productoExistente = await repositorio.GetProductoAsync(codProducto);
            if (productoExistente is null)
            {
                return NotFound();
            }

            productoExistente.Nombre = productoDTO.Nombre;
            productoExistente.Descripcion = productoDTO.Descripcion;
            productoExistente.Precio = productoDTO.Precio;
            await repositorio.ModificarProductoAsync(productoExistente);

            return productoExistente.ConvertirDTO();
        }

        [HttpDelete]
        public async Task<ActionResult> BorrarProducto(string codProducto)
        {
            Producto productoExistente = await repositorio.GetProductoAsync(codProducto);
            if (productoExistente is null)
            {
                return NotFound();
            }
            await repositorio.BorrarProductoAsync(codProducto);

            return NoContent();
        }
    }
}
