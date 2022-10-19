using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi01.Models;
using TestApi01.Repository;

namespace TestApi01.Controllers
{
    [Route("productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductosEnMemoria repositorio;

        public ProductosController()
        {
            repositorio = new ProductosEnMemoria();
        }

        [HttpGet]
        public IEnumerable<Producto> GetProductos()
        {
            var result = repositorio.GetProductos();
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<Producto> GetProducto(int id)
        {
            var result = repositorio.GetProducto(id);

            if (result is null)
            {
                return NotFound();
            }

            return result;
        }
    }
}
