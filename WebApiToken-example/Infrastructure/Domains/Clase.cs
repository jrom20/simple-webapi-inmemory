using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ngServer.Infrastructure.Domains
{
    public class Clase
    {
        public Clase()
        {
            Secciones = new List<Seccion>();
        }
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        
        [JsonProperty("horario")]
        public string Horario { get; set; }

        [JsonProperty("ubicacion")]
        public Ubicacion Ubicacion { get; set; }

        [JsonProperty("secciones")]
        public ICollection<Seccion> Secciones { get; set; }
    }

    public class Ubicacion
    {
        public int Id { get; set; }
        public string Aula { get; set; }
        public string Campus { get; set; }
    }

    public partial class Seccion
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("profesor")]
        public string Profesor { get; set; }

        [JsonProperty("descripcion")]
        public string Descripcion { get; set; }
    }
}
