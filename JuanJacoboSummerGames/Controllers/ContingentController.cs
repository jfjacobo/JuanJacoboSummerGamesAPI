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
    public class ContingentController : ControllerBase
    {
        private readonly SummerGamesContext _context;

        public ContingentController(SummerGamesContext context)
        {
            _context = context;
        }

        // GET: api/Contingent
        //Getting just the Contingent Information
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContingentDTO>>> GetContingents()
        {
            return await _context.Contingents.Select(c => new ContingentDTO
            {
                ID = c.ID,
                Code = c.Code,
                Name = c.Name,
            }).ToListAsync();
        }

        // GET: api/Contingent/inc - Include Athlete and Sport Collection
        [HttpGet("inc")]
        public async Task<ActionResult<IEnumerable<ContingentDTO>>> GetContingentsInc()
        {
            return await _context.Contingents
                .Include(c => c.Athletes)
                .Select(c => new ContingentDTO
                {
                    ID = c.ID,
                    Code = c.Code,
                    Name = c.Name,
                    Athletes = c.Athletes.Select(cAthlete => new AthleteDTO
                    {
                        ID = cAthlete.ID,
                        FirstName = cAthlete.FirstName,
                        MiddleName = cAthlete.MiddleName,
                        LastName = cAthlete.LastName,
                        AthleteCode = cAthlete.AthleteCode,
                        DOB = cAthlete.DOB,
                        Height = cAthlete.Height,
                        Weight = cAthlete.Weight,
                        Affiliation = cAthlete.Affiliation,
                        MediaInfo = cAthlete.MediaInfo,
                        Gender = cAthlete.Gender,
                        ContingentID = cAthlete.ContingentID,
                        SportID = cAthlete.SportID,
                        Sport = new SportDTO
                        {
                            ID = cAthlete.Sport.ID,
                            Code = cAthlete.Sport.Code,
                            Name = cAthlete.Sport.Name
                        }
                    }).ToList()
                }).ToListAsync();
        }
        // GET: api/Contingent/5
        //Getting only the Contingent information with the ID we select
        [HttpGet("{id}")]
        public async Task<ActionResult<ContingentDTO>> GetContingent(int id)
        {
            var contingentDTO = await _context.Contingents
                .Select(c => new ContingentDTO
                {
                    ID = c.ID,
                    Code = c.Code,
                    Name = c.Name,
                }).FirstOrDefaultAsync(c => c.ID == id);

            if (contingentDTO == null)
            {
                return NotFound(new { message = "Error: Contingent not found" });
            }

            return contingentDTO;
        }
        // GET: api/Contingent/inc/5
        //Getting Contingent and Athlete Information
        [HttpGet("inc/{id}")]
        public async Task<ActionResult<ContingentDTO>> GetContingentInc(int id)
        {
            var contingentDTO = await _context.Contingents
                .Include(c => c.Athletes)
                .Select(c => new ContingentDTO
                {
                    ID = c.ID,
                    Code = c.Code,
                    Name = c.Name,
                    Athletes = c.Athletes.Select(cAthlete => new AthleteDTO
                    {
                        ID = cAthlete.ID,
                        FirstName = cAthlete.FirstName,
                        MiddleName = cAthlete.MiddleName,
                        LastName = cAthlete.LastName,
                        AthleteCode = cAthlete.AthleteCode,
                        DOB = cAthlete.DOB,
                        Height = cAthlete.Height,
                        Weight = cAthlete.Weight,
                        Affiliation = cAthlete.Affiliation,
                        MediaInfo = cAthlete.MediaInfo,
                        Gender = cAthlete.Gender,
                        ContingentID = cAthlete.ContingentID,
                        SportID = cAthlete.SportID,
                        Sport = new SportDTO
                        {
                            ID = cAthlete.Sport.ID,
                            Code = cAthlete.Sport.Code,
                            Name = cAthlete.Sport.Name
                        }
                    }).ToList()
                }).FirstOrDefaultAsync(c => c.ID == id);

            if (contingentDTO == null)
            {
                return NotFound(new { message = "Error: Contingent not found" });
            }

            return contingentDTO;
        }

        private bool ContingentExists(int id)
        {
            return _context.Contingents.Any(e => e.ID == id);
        }
    }
}
