using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi01.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public DateTime FechaAlta { get; set; }
        /// <summary>
        /// Basicamente es un id alfanumerico del producto que también sirve como identificador y para buscar el producto y 
        /// así no mostrar el id de la BD
        /// </summary>
        public string SKU { get; set; }
    }
}
