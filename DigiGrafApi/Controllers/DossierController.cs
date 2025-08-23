using DigiGrafWeb.Data;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DossierController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly FuneralSession _session;

        public DossierController(AppDbContext db, FuneralSession session)
        {
            _db = db;
            _session = session;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateNew([FromBody] Dossier request)
        {
            if (request == null)
                return BadRequest("Invalid dossier data.");

            // Save Dossier to database
            await _db.Dossiers.AddAsync(request);
            await _db.SaveChangesAsync();

            // Update session info
            _session.FuneralNumber = request.FuneralNumber;
            _session.FuneralLeader = request.FuneralLeader;
            _session.Voorregeling = request.Voorregeling;
            _session.FuneralType = request.FuneralType;
            _session.NewDossierCreation = true;
            _session.DossierCompleted = false;

            return Ok(request);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDossier(int id)
        {
            var dossier = await _db.Dossiers.FindAsync(id);

            if (dossier == null)
                return NotFound();

            return Ok(dossier);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDossier(int id, [FromBody] Dossier updatedDossier)
        {
            var dossier = await _db.Dossiers
                .Include(d => d.Deceased)  // include the related Deceased entity
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dossier == null) return NotFound();

            // Update deceased info
            dossier.Deceased.FirstName = updatedDossier.Deceased.FirstName;
            dossier.Deceased.LastName = updatedDossier.Deceased.LastName;
            dossier.Deceased.Dob = updatedDossier.Deceased.Dob;
            // ...other deceased fields

            // Update dossier info
            dossier.FuneralLeader = updatedDossier.FuneralLeader;
            dossier.FuneralNumber = updatedDossier.FuneralNumber;
            dossier.Voorregeling = updatedDossier.Voorregeling;
            dossier.FuneralType = updatedDossier.FuneralType;

            await _db.SaveChangesAsync();
            return Ok(dossier);
        }
    }
}
