using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API_STPS.Data;
using API_STPS.Models;
using Microsoft.SqlServer.Server;


namespace API_STPS.Controllers
{

    [ApiController]
    [Route("api/incisos")]
    public class IncisosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IncisosController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*
        [HttpGet]
        public IEnumerable<Normas> Get
        public async Task<IActionResult> Index()
        {
            return View(await _context.Normas.ToListAsync());
        }

      */

        [HttpPost]
        public async Task<ActionResult> Post(Incisos_normas inciso)
        {
            _context.Add(inciso);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Incisos_normas>> Get(int id)
        {
            var inciso = await _context.Normas.FirstOrDefaultAsync(x => x.Id == id);

            if (inciso is null)
            {
                return NotFound();
            }

            return Ok(inciso);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incisos_normas>>> Get()
        {
            // Obtener todos los registros de la base de datos
            var inciso = await _context.Incisos_normas.ToListAsync();

            // Si no hay registros, devolver un NotFound
            if (inciso == null || !inciso.Any())
            {
                return NotFound();
            }

            // Devolver todos los registros como JSON (esto es lo que devuelve por defecto)
            return Ok(inciso);
        }

        [HttpGet("busqueda_inciso")]
        public async Task<ActionResult<IEnumerable<Incisos_normas>>> GetInciso([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("El término de búsqueda no puede estar vacío.");
            }

            try
            {
                // Filtrar las normas por el término de búsqueda en 'nombre_noms' o 'descripcion'
                var inciso = await _context.Incisos_normas
                    .Where(n => n.inciso_noms.Contains(searchTerm) || n.descripcion.Contains(searchTerm))
                    .ToListAsync();

                if (inciso == null || inciso.Count == 0)
                {
                    return NotFound("No se encontraron normas que coincidan con el término de búsqueda.");
                }

                return Ok(inciso);
            }
            catch (Exception ex)
            {
                // Registrar el error para depuración
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut]
        public void PutInciso(Incisos_normas inciso)
        {
            var incisoExistente = _context.Incisos_normas.Find(inciso.Id);
            if (incisoExistente != null)
            {
                incisoExistente.id_noms = inciso.id_noms;
                incisoExistente.inciso_noms = inciso.inciso_noms;
                incisoExistente.descripcion = inciso.descripcion;
                incisoExistente.comprobacion = inciso.comprobacion;
                incisoExistente.criterio_acepton = inciso.criterio_acepton;
                incisoExistente.observacion = inciso.observacion;


                _context.SaveChanges();
            }
        }

        [HttpDelete("{id:int}")]
        public void DeleteInciso(int id)
        {
            var inciso = _context.Incisos_normas.Find(id);
            if (inciso != null)
            {
                _context.Incisos_normas.Remove(inciso);
                _context.SaveChanges();
            }
        }

    }
}
