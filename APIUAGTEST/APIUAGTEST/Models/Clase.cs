using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIUAGTEST.Models
{
    public class Clase
    {
        public int IdTrayectoria { get; set; }
        public string CrseId { get; set; }
        public string EmplId { get; set; }
        public string AcadCareer { get; set; }
        public string Institution { get; set; }
        public string STRM { get; set; }
        public string ClassNbr { get; set; }
        public string GradeCategory { get; set; }
        public string AcadProg { get; set; }
        public string DesCurso { get; set; }
    }
}
