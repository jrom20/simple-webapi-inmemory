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
            
            if(Startup.Secciones.Count == 0)
                Startup.Secciones = JsonConvert.DeserializeObject<List<Seccion>>(currentSecciones);
        }

        [HttpGet]
        public ActionResult<List<Seccion>> Get()
        {
            return Startup.Secciones;
        }


        [HttpPost]
        public ActionResult<string> Post(Seccion nuevaSeccion)
        {
            var lastId = Startup.Secciones.OrderByDescending(c => c.Id).FirstOrDefault().Id;
            nuevaSeccion.Id = lastId + 1;

            Startup.Secciones.Add(nuevaSeccion);

            return "OK";
        }
    }
}
