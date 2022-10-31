﻿using Microsoft.AspNetCore.Http;
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
        public IEnumerable<ProductoDTO> GetProductos()
        {
            var result = repositorio.GetProductos().Select(p => p.ConvertirDTO());
            return result;
        }

        [HttpGet("{codProducto}")]
        public ActionResult<ProductoDTO> GetProducto(string codProducto)
        {
            var result = repositorio.GetProducto(codProducto).ConvertirDTO();

            if (result is null)
            {
                return NotFound();
            }

            return result;
        }
    }
}
