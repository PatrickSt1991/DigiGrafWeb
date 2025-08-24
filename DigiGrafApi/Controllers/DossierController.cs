using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Mappers;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DossierController : ControllerBase
{
    private readonly FuneralSession _session;
    private readonly AppDbContext _db;

    public DossierController(AppDbContext db, FuneralSession session)
    {
        _db = db;
        _session = session;
    }
    
    //Create Dossier
    [HttpPost("new")]
    public async Task<IActionResult> CreateNew([FromBody] DossierDto request)
    {
        if (request == null)
            return BadRequest("Invalid dossier data");

        var dossier = DossierMapper.ToEntity(request);

        _db.Dossiers.Add(dossier);
        await _db.SaveChangesAsync();

        // Update session
        _session.FuneralLeader = dossier.FuneralLeader;
        _session.FuneralNumber = dossier.FuneralNumber;
        _session.NewDossierCreation = true;
        _session.DossierCompleted = false;
        _session.Voorregeling = dossier.Voorregeling;

        return Ok(DossierMapper.ToDto(dossier));
    }

    //Get Dossier
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid Id)
    {
        var dossier = await _db.Dossiers
            .Include(d => d.Deceased)
            .Include(d => d.DeathInfo)
            .FirstOrDefaultAsync(d => d.Id == Id);

        if (dossier == null)
            return NotFound(new { message = "Dossier not found" });

        return Ok(DossierMapper.ToDto(dossier));
    }

    //Update Dossier
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateDossier(Guid Id, [FromBody] DossierDto request)
    {
        var dossier = await _db.Dossiers
            .Include(d => d.Deceased)
            .Include(d => d.DeathInfo)
            .FirstOrDefaultAsync(d => d.Id == Id);

        if (dossier == null)
            return NotFound(new { message = "Dossier not found" });

        var updatedEntity = DossierMapper.ToEntity(request);

        dossier.FuneralLeader = updatedEntity.FuneralLeader;
        dossier.FuneralNumber = updatedEntity.FuneralNumber;
        dossier.FuneralType = updatedEntity.FuneralType;
        dossier.Voorregeling = updatedEntity.Voorregeling;
        dossier.DossierCompleted = updatedEntity.DossierCompleted;

        dossier.Deceased = updatedEntity.Deceased;
        dossier.DeathInfo = updatedEntity.DeathInfo;

        await _db.SaveChangesAsync();

        return Ok(DossierMapper.ToDto(dossier));
    }
}
