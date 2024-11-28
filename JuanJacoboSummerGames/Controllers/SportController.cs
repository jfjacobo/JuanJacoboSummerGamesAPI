using System;
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
    public class SportController : ControllerBase
    {
        private readonly SummerGamesContext _context;

        public SportController(SummerGamesContext context)
        {
            _context = context;
        }

        // GET: api/Sport
        //Getting only the Sports Information
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportDTO>>> GetSports()
        {
            return await _context.Sports.Select(s => new SportDTO
            {
                ID = s.ID,
                Code = s.Code,
                Name = s.Name
            }).ToListAsync();
        }
        // GET: api/Sport/inc - Include Athlete and Contingent Collection
        [HttpGet("inc")]
        public async Task<ActionResult<IEnumerable<SportDTO>>> GetSportsInc()
        {
            return await _context.Sports.Include(s => s.Athletes).Select(s => new SportDTO
            {
                ID = s.ID,
                Code = s.Code,
                Name = s.Name,
                Athletes = s.Athletes.Select(sAthlete => new AthleteDTO
                {
                    ID = sAthlete.ID,
                    FirstName = sAthlete.FirstName,
                    MiddleName = sAthlete.MiddleName,
                    LastName = sAthlete.LastName,
                    AthleteCode = sAthlete.AthleteCode,
                    DOB = sAthlete.DOB,
                    Height = sAthlete.Height,
                    Weight = sAthlete.Weight,
                    Affiliation = sAthlete.Affiliation,
                    MediaInfo = sAthlete.MediaInfo,
                    Gender = sAthlete.Gender,
                    ContingentID = sAthlete.ContingentID,
                    Contingent = new ContingentDTO
                    {
                        ID = sAthlete.Contingent.ID,
                        Code = sAthlete.Contingent.Code,
                        Name = sAthlete.Contingent.Name
                    },
                    SportID = sAthlete.SportID
                }).ToList()
            }).ToListAsync();
        }
        // GET: api/Sport/5
        //Getting only the Sport information with the ID we select
        [HttpGet("{id}")]
        public async Task<ActionResult<SportDTO>> GetSport(int id)
        {
            var sportDTO = await _context.Sports
               .Select(s => new SportDTO
               {
                   ID = s.ID,
                   Code = s.Code,
                   Name = s.Name,
               }).FirstOrDefaultAsync(s => s.ID == id);

            if (sportDTO == null)
            {
                return NotFound(new { message = "Error: Sport not found" });
            }

            return sportDTO;
        }

        // GET: api/Sport/inc/5
        //Getting Sport and Athlete information
        [HttpGet("inc/{id}")]
        public async Task<ActionResult<SportDTO>> GetSportInc(int id)
        {
            var sportDTO = await _context.Sports
                .Include(s => s.Athletes)
               .Select(s => new SportDTO
               {
                   ID = s.ID,
                   Code = s.Code,
                   Name = s.Name,
                   Athletes = s.Athletes.Select(sAthlete => new AthleteDTO
                   {
                       ID = sAthlete.ID,
                       FirstName = sAthlete.FirstName,
                       MiddleName = sAthlete.MiddleName,
                       LastName = sAthlete.LastName,
                       AthleteCode = sAthlete.AthleteCode,
                       DOB = sAthlete.DOB,
                       Height = sAthlete.Height,
                       Weight = sAthlete.Weight,
                       Affiliation = sAthlete.Affiliation,
                       MediaInfo = sAthlete.MediaInfo,
                       Gender = sAthlete.Gender,
                       ContingentID = sAthlete.ContingentID,
                       Contingent = new ContingentDTO
                       {
                           ID = sAthlete.Contingent.ID,
                           Code = sAthlete.Contingent.Code,
                           Name = sAthlete.Contingent.Name,
                       },
                       SportID = sAthlete.SportID
                   }).ToList()
               }).FirstOrDefaultAsync(s => s.ID == id);

            if (sportDTO == null)
            {
                return NotFound(new { message = "Error: Sport not found" });
            }

            return sportDTO;
        }

        private bool SportExists(int id)
        {
            return _context.Sports.Any(e => e.ID == id);
        }
    }
}
