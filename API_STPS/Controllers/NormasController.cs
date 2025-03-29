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
    [Route("api/normas")]
    public class NormasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public NormasController(ApplicationDbContext context)
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
        public async Task <ActionResult> Post(Normas normas)
        {
            _context.Add(normas);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task <ActionResult<Normas>> Get(int id)
        {
            var norma = await _context.Normas.FirstOrDefaultAsync(x => x.Id == id);

            if (norma is null)
            {
                return NotFound();
            }

            return Ok(norma);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Normas>>> Get()
        {
            // Obtener todos los registros de la base de datos
            var normas = await _context.Normas.ToListAsync();

            // Si no hay registros, devolver un NotFound
            if (normas == null || !normas.Any())
            {
                return NotFound();
            }

            // Devolver todos los registros como JSON (esto es lo que devuelve por defecto)
            return Ok(normas);
        }

        [HttpGet("busqueda_noms")]
        public async Task<ActionResult<IEnumerable<Normas>>> GetNormas([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("El término de búsqueda no puede estar vacío.");
            }

            try
            {
                // Filtrar las normas por el término de búsqueda en 'nombre_noms' o 'descripcion'
                var normas = await _context.Normas
                    .Where(n => n.nombre_noms.Contains(searchTerm) || n.descripcion.Contains(searchTerm))
                    .ToListAsync();

                if (normas == null || normas.Count == 0)
                {
                    return NotFound("No se encontraron normas que coincidan con el término de búsqueda.");
                }

                return Ok(normas);
            }
            catch (Exception ex)
            {
                // Registrar el error para depuración
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


    }
}
