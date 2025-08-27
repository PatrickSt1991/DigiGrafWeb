using DigiGrafWeb.Data;
using DigiGrafWeb.DTOs;
using DigiGrafWeb.Mappers;
using DigiGrafWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiGrafWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DossierController(AppDbContext db, FuneralSession session) : ControllerBase
    {

        //Create Dossier
        [HttpPost("new")]
        public async Task<IActionResult> CreateNew([FromBody] DossierDto request)
        {
            if (request == null)
                return BadRequest("Invalid dossier data");

            request.Id = Guid.NewGuid();
            if (request.Deceased != null) request.Deceased.Id = Guid.NewGuid();
            if (request.DeathInfo != null) request.DeathInfo.Id = Guid.NewGuid();

            var dossier = DossierMapper.ToEntity(request);

            db.Dossiers.Add(dossier);
            await db.SaveChangesAsync();

            // Update session
            session.FuneralLeader = dossier.FuneralLeader;
            session.FuneralNumber = dossier.FuneralNumber;
            session.NewDossierCreation = true;
            session.DossierCompleted = false;
            session.Voorregeling = dossier.Voorregeling;

            return Ok(DossierMapper.ToDto(dossier));
        }

        //Get Dossier
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var dossier = await db.Dossiers
                .Include(d => d.Deceased)
                .Include(d => d.DeathInfo)
                .FirstOrDefaultAsync(d => d.Id == Id);

            if (dossier == null)
                return NotFound(new { message = "Dossier not found" });

            return Ok(DossierMapper.ToDto(dossier));
        }

        //Update Dossier
        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> UpdateDossier(Guid id, [FromBody] DossierDto dto)
        {
            var dossier = await db.Dossiers
                .Include(d => d.Deceased)
                .Include(d => d.DeathInfo)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (dossier == null)
                return NotFound(new { message = "Dossier not found" });

            // Update top-level Dossier fields if present
            if (!string.IsNullOrWhiteSpace(dto.FuneralLeader)) dossier.FuneralLeader = dto.FuneralLeader;
            if (!string.IsNullOrWhiteSpace(dto.FuneralNumber)) dossier.FuneralNumber = dto.FuneralNumber;
            if (!string.IsNullOrWhiteSpace(dto.FuneralType)) dossier.FuneralType = dto.FuneralType;
            dossier.Voorregeling = dto.Voorregeling;
            dossier.DossierCompleted = dto.DossierCompleted;

            // Update Deceased fields if present
            if (dto.Deceased != null)
            {
                if (dossier.Deceased == null) dossier.Deceased = new Deceased { Id = Guid.NewGuid() };

                var d = dossier.Deceased;
                var dd = dto.Deceased;

                if (!string.IsNullOrWhiteSpace(dd.FirstName)) d.FirstName = dd.FirstName;
                if (!string.IsNullOrWhiteSpace(dd.LastName)) d.LastName = dd.LastName;
                if (!string.IsNullOrWhiteSpace(dd.Salutation)) d.Salutation = dd.Salutation;
                if (dd.DOB != null) d.Dob = dd.DOB;
                if (!string.IsNullOrWhiteSpace(dd.PlaceOfBirth)) d.PlaceOfBirth = dd.PlaceOfBirth;
                if (!string.IsNullOrWhiteSpace(dd.PostalCode)) d.PostalCode = dd.PostalCode;
                if (!string.IsNullOrWhiteSpace(dd.Street)) d.Street = dd.Street;
                if (!string.IsNullOrWhiteSpace(dd.HouseNumber)) d.HouseNumber = dd.HouseNumber;
                if (!string.IsNullOrWhiteSpace(dd.HouseNumberAddition)) d.HouseNumberAddition = dd.HouseNumberAddition;
                if (!string.IsNullOrWhiteSpace(dd.City)) d.City = dd.City;
                if (!string.IsNullOrWhiteSpace(dd.County)) d.County = dd.County;
                d.HomeDeceased = dd.HomeDeceased;
            }

            // Update DeathInfo fields if present
            if (dto.DeathInfo != null)
            {
                if (dossier.DeathInfo == null) dossier.DeathInfo = new DeathInfo { Id = Guid.NewGuid() };

                var di = dossier.DeathInfo;
                var ddi = dto.DeathInfo;

                if (ddi.DateOfDeath != null) di.DateOfDeath = ddi.DateOfDeath;
                if (ddi.TimeOfDeath != null) di.TimeOfDeath = ddi.TimeOfDeath;
                if (!string.IsNullOrWhiteSpace(ddi.LocationOfDeath)) di.LocationOfDeath = ddi.LocationOfDeath;
                if (!string.IsNullOrWhiteSpace(ddi.PostalCodeOfDeath)) di.PostalCodeOfDeath = ddi.PostalCodeOfDeath;
                if (!string.IsNullOrWhiteSpace(ddi.HouseNumberOfDeath)) di.HouseNumberOfDeath = ddi.HouseNumberOfDeath;
                if (!string.IsNullOrWhiteSpace(ddi.HouseNumberAdditionOfDeath)) di.HouseNumberAdditionOfDeath = ddi.HouseNumberAdditionOfDeath;
                if (!string.IsNullOrWhiteSpace(ddi.StreetOfDeath)) di.StreetOfDeath = ddi.StreetOfDeath;
                if (!string.IsNullOrWhiteSpace(ddi.CityOfDeath)) di.CityOfDeath = ddi.CityOfDeath;
                if (!string.IsNullOrWhiteSpace(ddi.CountyOfDeath)) di.CountyOfDeath = ddi.CountyOfDeath;
                if (!string.IsNullOrWhiteSpace(ddi.BodyFinding)) di.BodyFinding = ddi.BodyFinding;
                if (!string.IsNullOrWhiteSpace(ddi.Origin)) di.Origin = ddi.Origin;
            }

            await db.SaveChangesAsync();

            return Ok(DossierMapper.ToDto(dossier));
        }

    }
}