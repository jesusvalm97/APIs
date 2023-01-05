using APIUAGTEST.BusinessLogic;
using APIUAGTEST.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APIUAGTEST.Controllers
{
    [Route("Alumno")]
    [ApiController]
    public class AlumnoController : Controller
    {
        private readonly IBLAcademia blAcademia;

        public AlumnoController(IBLAcademia BLAcademia)
        {
            blAcademia = BLAcademia;
        }

        [HttpGet("GetClases")]
        public IEnumerable<Clase> GetClases()
        {
            return blAcademia.GetClases();
        }
    }
}
