using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestApi01.DTO;
using TestApi01.Models;
using TestApi01.Repository;

namespace TestApi01.Controllers
{
    [Route("productos")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosEnMemoria repositorio;

        public ProductosController(IProductosEnMemoria r)
        {
            repositorio = r;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<ProductoDTO>> GetProductos()
        {
            var result = (await repositorio.GetProductosAsync()).Select(p => p.ConvertirDTO());
            return result;
        }

        [HttpGet("{codProducto}")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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

        [HttpPost("GuardarImagen")]
        public async Task<string> GuardarImagen([FromForm] SubirImagenAPI fichero)
        {
            string ruta = string.Empty;

            if(fichero.Archivo.Length > 0)
            {
                string nombreArchivo = Guid.NewGuid().ToString() + ".jpg";
                ruta = $"Imagenes/{nombreArchivo}";

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    await fichero.Archivo.CopyToAsync(stream);
                }
            }

            return ruta;
        }
    }
}
