using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class ClasesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Seccion>> Get()
        {
            string currentSecciones = @"[{""id"":1,""name"":""Experiencia de Usuario"",""time"":""6:40 pm"",""location"":{""room"":""LSW"",""campus"":""SPS""},""ImageUrl"":""\/assets\/basic-shield.png"",""secciones"":[{""id"":1,""name"":""PX-04"",""profesor"":""Juan Romero"",""duracion"":""1 hora"",""descripcion"":""Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus egestas nisi sem, eget posuere massa dictum quis. Nam laoreet quis urna id consequat. Donec""},{""id"":2,""name"":""PX-006"",""profesor"":""Juan Romero"",""duracion"":""1 hora"",""descripcion"":""Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus egestas nisi sem, eget posuere massa dictum quis. Nam laoreet quis urna id consequat. Donec""}]},{""id"":2,""name"":""Matematicas discretas"",""time"":""8:40 am"",""location"":{""room"":""305"",""campus"":""SPS""},""ImageUrl"":""\/assets\/basic-shield.png""},{""id"":3,""name"":""Fisica I"",""time"":""6:40 pm"",""location"":{""room"":""211"",""campus"":""SPS""},""ImageUrl"":""\/assets\/basic-shield.png""},{""id"":4,""name"":""Fisica II"",""time"":""5:20 pm"",""location"":{""room"":""310"",""campus"":""SPS""},""ImageUrl"":""\/assets\/basic-shield.png""}]";

            return JsonConvert.DeserializeObject<List<Seccion>>(currentSecciones);
        }
    }


    public partial class Seccion
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("secciones", NullValueHandling = NullValueHandling.Ignore)]
        public Seccione[] Secciones { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("campus")]
        public string Campus { get; set; }
    }

    public partial class Seccione
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("profesor")]
        public string Profesor { get; set; }

        [JsonProperty("duracion")]
        public string Duracion { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }
    }
}
