using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ngServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClasesController : ControllerBase
    {
        private readonly ILogger<ClasesController> _logger;
        public ClasesController(ILogger<ClasesController> logger)
        {
            _logger = logger;
            string currentSecciones = @"[{""id"":1,""name"":""Experiencia de Usuario"",""time"":""6:40 pm"",""location"":{""room"":""LSW"",""campus"":""SPS""},""ImageUrl"":""\/assets\/basic-shield.png"",""secciones"":[{""id"":1,""name"":""PX-04"",""profesor"":""Juan Romero"",""duracion"":""1 hora"",""descripcion"":""Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus egestas nisi sem, eget posuere massa dictum quis. Nam laoreet quis urna id consequat. Donec""},{""id"":2,""name"":""PX-006"",""profesor"":""Juan Romero"",""duracion"":""1 hora"",""descripcion"":""Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus egestas nisi sem, eget posuere massa dictum quis. Nam laoreet quis urna id consequat. Donec""}]},{""id"":2,""name"":""Matematicas discretas"",""time"":""8:40 am"",""location"":{""room"":""305"",""campus"":""SPS""},""ImageUrl"":""\/assets\/basic-shield.png""},{""id"":3,""name"":""Fisica I"",""time"":""6:40 pm"",""location"":{""room"":""211"",""campus"":""SPS""},""ImageUrl"":""\/assets\/basic-shield.png""},{""id"":4,""name"":""Fisica II"",""time"":""5:20 pm"",""location"":{""room"":""310"",""campus"":""SPS""},""ImageUrl"":""\/assets\/basic-shield.png""}]";
            
            if(Startup.Clases.Count == 0)
                Startup.Clases = JsonConvert.DeserializeObject<List<Clase>>(currentSecciones);
        }

        [HttpGet]
        public ActionResult<List<Clase>> Get()
        {
            return Startup.Clases;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Clase> Get(int id)
        {
            return Startup.Clases.FirstOrDefault(c => c.Id == id);
        }


        [HttpPost]
        [Route("{idClase}/secciones")]
        public ActionResult<string> PostNewSeccion(int idClase, Seccion seccion)
        {
            //he did some changes here
            var caseActual = Startup.Clases.FirstOrDefault(c => c.Id == idClase);
            //he did some changes here
            if (caseActual != null)
            {
                var lastId = caseActual.Secciones.OrderByDescending(c => c.Id).FirstOrDefault().Id;
                seccion.Id = lastId + 1;
                caseActual.Secciones.Add(seccion);
                
                return "OK";
                //he did some changes here
            }
            else
            {
                //he did some changes here
                return "Failed";
            }
            
        }


        [HttpPost]
        public ActionResult<string> Post(Clase nuevaSeccion)
        {
            var lastId = Startup.Clases.OrderByDescending(c => c.Id).FirstOrDefault().Id;
            nuevaSeccion.Id = lastId + 1;

            Startup.Clases.Add(nuevaSeccion);

            return "OK";
        }
    }
}
