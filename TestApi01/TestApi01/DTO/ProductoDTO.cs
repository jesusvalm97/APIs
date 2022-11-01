using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi01.DTO
{
    public class ProductoDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        [Range(2, 100, ErrorMessage = "El valor {0} tiene que estar entre {1} y {2}.")]
        public double Precio { get; set; }
        [Required]
        /// <summary>
        /// Basicamente es un id alfanumerico del producto que también sirve como identificador y para buscar el producto y 
        /// así no mostrar el id de la BD
        /// </summary>
        public string SKU { get; set; }
    }
}
