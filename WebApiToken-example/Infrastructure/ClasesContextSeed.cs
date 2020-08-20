using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ngServer.Infrastructure.Contexts;
using ngServer.Infrastructure.Domains;

namespace ngServer.Infrastructure
{
    public class ClasesContextSeed
    {
        private readonly ClasesContext _ctxData;

        public ClasesContextSeed(ClasesContext ctxData)
        {
            _ctxData = ctxData;
        }
        public void SeedAsync()
        {
            _ctxData.Database.EnsureCreated();
            //_ctxData.Database.Migrate();
            if (!_ctxData.Clases.Any())
            {
                Clase clase = new Clase();
                clase.Horario = "6:40 PM";
                clase.Nombre = "Experiencia Usuario";

                clase.Ubicacion = new Ubicacion()
                {
                    Aula = "309",
                    Campus = "SPS"
                };

                clase.Secciones.Add(new Seccion()
                {
                    Descripcion = "Experiencia Usuario",
                    Nombre = "198",
                    Profesor = "Juan Romero"
                });

                _ctxData.Clases.Add(clase);
            }

            _ctxData.SaveChanges();
        }
    }
}
