﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JuanJacoboSummerGames.Data;
using JuanJacoboSummerGames.Models;

namespace JuanJacoboSummerGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        private readonly SummerGamesContext _context;

        public AthleteController(SummerGamesContext context)
        {
            _context = context;
        }

        // GET: api/Athlete
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AthleteDTO>>> GetAthletes()
        {
            return await _context.Athletes
                .Include(a => a.Sport)
                .Include(a => a.Contingent)
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Affiliation = a.Affiliation,
                    MediaInfo = a.MediaInfo,
                    Gender = a.Gender,
                    RowVersion = a.RowVersion,
                    ContingentID = a.ContingentID,
                    Contingent = new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name,
                    },
                    SportID = a.SportID,
                    Sport = new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name,
                    }
                })
                .ToListAsync();
        }

        // GET: api/Athlete/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AthleteDTO>> GetAthlete(int id)
        {
            var athleteDTO = await _context.Athletes
                .Include(a => a.Sport)
                .Include(a => a.Contingent)
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Affiliation = a.Affiliation,
                    MediaInfo = a.MediaInfo,
                    Gender = a.Gender,
                    RowVersion = a.RowVersion,
                    ContingentID = a.ContingentID,
                    Contingent = new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name,
                    },
                    SportID = a.SportID,
                    Sport = new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name,
                    }
                })
                .FirstOrDefaultAsync(a => a.ID == id);
            if (athleteDTO == null)
            {
                return NotFound(new { message = "Error: Athlete record not found." });
            }

            return athleteDTO;
        }

        // GET: api/AthleteBySport
        [HttpGet("BySport/{id}")]
        public async Task<ActionResult<IEnumerable<AthleteDTO>>> GetAthleteBySport(int id)
        {
            var athleteDTOs = await _context.Athletes
                .Include(a => a.Sport).Include(a => a.Contingent)
                .Where(a => a.SportID == id)
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Affiliation = a.Affiliation,
                    MediaInfo = a.MediaInfo,
                    Gender = a.Gender,
                    ContingentID = a.ContingentID,
                    Contingent = new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name,
                    },
                    SportID = a.SportID,
                    Sport = new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name,
                    }
                }).ToListAsync();

            if (athleteDTOs.Count() > 0)
            {
                return athleteDTOs;
            }
            else
            {
                return NotFound(new { message = "Error: No Athlete records for that Sport." });
            }
        }
        // GET: api/AthleteByContingent
        [HttpGet("ByContingent/{id}")]
        public async Task<ActionResult<IEnumerable<AthleteDTO>>> GetAthleteByContingent(int id)
        {
            var athleteDTOs = await _context.Athletes
                .Include(a => a.Sport).Include(a => a.Contingent)
                .Where(a => a.ContingentID == id)
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Affiliation = a.Affiliation,
                    MediaInfo = a.MediaInfo,
                    Gender = a.Gender,
                    ContingentID = a.ContingentID,
                    Contingent = new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name,
                    },
                    SportID = a.SportID,
                    Sport = new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name,
                    }
                }).ToListAsync();

            if (athleteDTOs.Count() > 0)
            {
                return athleteDTOs;
            }
            else
            {
                return NotFound(new { message = "Error: No Athlete records for that Conitngent." });
            }
        }


        // PUT: api/Athlete/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAthlete(int id, AthleteDTO athleteDTO)
        {
            if (id != athleteDTO.ID)
            {
                return BadRequest(new { message = "Error: ID does not match Athlete" });
            }

            //_context.Entry(athlete).State = EntityState.Modified;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Get the record you want to update
            var athleteToUpdate = await _context.Athletes.FindAsync(id);
            //Check that you got it
            if (athleteToUpdate == null)
            {
                return NotFound(new { message = "Error: Athlete record not found." });
            }

            //Checking the database for concurrency errors
            //Check if someone else have made updates before you
            //Note using SequenceEqual becuase it is an array after all.
            if (athleteToUpdate.RowVersion != null)
            {
                if (!athleteToUpdate.RowVersion.SequenceEqual(athleteToUpdate.RowVersion))
                {
                    return Conflict(new { message = "Concurrency Error: Athlete has been changed by another user.  Try editing the record again." });
                }
            }
            athleteToUpdate.ID = athleteDTO.ID;
            athleteToUpdate.FirstName = athleteDTO.FirstName;
            athleteToUpdate.MiddleName = athleteDTO.MiddleName;
            athleteToUpdate.LastName = athleteDTO.LastName;
            athleteToUpdate.AthleteCode = athleteDTO.AthleteCode;
            athleteToUpdate.DOB = athleteDTO.DOB;
            athleteToUpdate.Height = athleteDTO.Height;
            athleteToUpdate.Weight = athleteDTO.Weight;
            athleteToUpdate.Affiliation = athleteDTO.Affiliation;
            athleteToUpdate.MediaInfo = athleteDTO.MediaInfo;
            athleteToUpdate.Gender = athleteDTO.Gender;
            athleteToUpdate.ContingentID = athleteDTO.ContingentID;
            athleteToUpdate.SportID = athleteDTO.SportID;

            //Put the original RowVersion value in the OriginalValues collection for the entity
            _context.Entry(athleteToUpdate).Property("RowVersion").OriginalValue = athleteDTO.RowVersion;
            //Ading try catch for more error handling
            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AthleteExists(id))
                {
                    return Conflict(new { message = "Concurrency Error: Athlete has been Removed." });
                }
                else
                {
                    return Conflict(new { message = "Concurrency Error: Athlete has been updated by another user.  Back out and try editing the record again." });
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE"))
                {
                    return BadRequest(new { message = "Unable to save: Duplicate Athlete Code number." });
                }
                else
                {
                    return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
                }
            }
        }



        // POST: api/Athlete
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AthleteDTO>> PostAthlete(AthleteDTO athleteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Athlete athlete = new Athlete //Using mapping generator
            {
                ID = athleteDTO.ID,
                FirstName = athleteDTO.FirstName,
                MiddleName = athleteDTO.MiddleName,
                LastName = athleteDTO.LastName,
                AthleteCode = athleteDTO.AthleteCode,
                DOB = athleteDTO.DOB,
                Height = athleteDTO.Height,
                Weight = athleteDTO.Weight,
                Affiliation = athleteDTO.Affiliation,
                MediaInfo = athleteDTO.MediaInfo,
                Gender = athleteDTO.Gender,
                ContingentID = athleteDTO.ContingentID,
                SportID = athleteDTO.SportID
            };

            try
            {
                _context.Athletes.Add(athlete);
                await _context.SaveChangesAsync();

                //Assign Database Generated values back into the DTO
                athleteDTO.ID = athlete.ID;
                athleteDTO.RowVersion = athlete.RowVersion;

                return CreatedAtAction(nameof(GetAthlete), new { id = athlete.ID }, athleteDTO);
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE"))
                {
                    return BadRequest(new { message = "Unable to save: Duplicate Athlete Code number." });
                }
                else
                {
                    return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
                }
            }
        }

        // DELETE: api/Athlete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAthlete(int id)
        {
            var athlete = await _context.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return NotFound(new { message = "Delete Error: Athlete has already been removed." });
            }
            try
            {
                _context.Athletes.Remove(athlete);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Delete Error: Unable to delete Athlete." });
            }
        }

        private bool AthleteExists(int id)
        {
            return _context.Athletes.Any(e => e.ID == id);
        }
    }
}
