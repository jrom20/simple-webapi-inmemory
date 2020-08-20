using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ngServer.Infrastructure.Contexts;
using ngServer.Infrastructure.Domains;

namespace ngServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //localhost/clase/1
    public class ClasesController : Controller
    {
        private readonly ClasesContext _dbContext;

        public ClasesController(ClasesContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<Clase>> Get()
        {
            return _dbContext.Clases
                .Include(c=> c.Ubicacion)
                .Include(c=> c.Secciones).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Clase> Get(int id)
        {
            return _dbContext.Clases.Include(c => c.Ubicacion)
                .Include(c => c.Secciones).FirstOrDefault(c=> c.Id == id);
        }


        [HttpPost]
        [Route("{idClase}/Secciones")]
        public ActionResult<Clase> PostNewSeccion(int idClase, Seccion seccion)
        {
            var clase = _dbContext.Clases
                .Include(c => c.Ubicacion)
                .Include(c => c.Secciones).FirstOrDefault(c => c.Id == idClase);

            clase.Secciones.Add(seccion);
            _dbContext.SaveChanges();
            return clase;
        }

        [HttpPut]
        [Route("{idSeccion}/Secciones")]
        public ActionResult<string> DeleteSeccion(int idSeccion)
        {
            var seccion = _dbContext.Secciones.FirstOrDefault(c => c.Id == idSeccion);
            _dbContext.Secciones.Remove(seccion);
            _dbContext.SaveChanges();

            return "";
        }

        [HttpPost]
        //localhost/clases
        public async Task<ActionResult<string>> Post(Clase nuevaClase)
        {
            await _dbContext.Clases.AddAsync(nuevaClase);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        
    }
}
