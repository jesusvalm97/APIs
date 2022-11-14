using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApi01.DTO;
using TestApi01.Models;

namespace TestApi01
{
    public static class Tools
    {
        /// <summary>
        /// Metodo para transformar/mapear un producto a productoDTO
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        //En el metodo al poner this en el paramatreo, indica que este metodo es una extensión de la clase producto
        public static ProductoDTO ConvertirDTO(this Producto producto)
        {
            if (producto != null)
            {
                return new ProductoDTO()
                {
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    SKU = producto.SKU
                };
            }

            return null;
        }

        public static UsuarioDTO ConvertirDTO(this UsuarioAPI usuario)
        {
            if (usuario != null)
            {
                return new UsuarioDTO()
                {
                    Usuario = usuario.Usuario,
                    Token = usuario.Token
                };
            }

            return null;
        }

        public static void Error(Exception exception, ILogger log)
        {
            log.LogError(exception.ToString());
            throw new Exception("Se produjo un error: " + exception.Message);
        }
    }
}
