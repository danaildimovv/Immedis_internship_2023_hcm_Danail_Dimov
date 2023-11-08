using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Repositories;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceLevelsController : ControllerBase
    {
        private readonly IExperienceLevelRepository _experienceLevelRepository;
        private readonly IMapper _mapper;
        public ExperienceLevelsController(IExperienceLevelRepository experienceLevelRepository, IMapper mapper)
        {
            _experienceLevelRepository = experienceLevelRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetExperienceLevels()
        {

            var experienceLevels = _mapper.Map<List<ExperienceLevelDTO>>(await _experienceLevelRepository.GetExperienceLevelsAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (experienceLevels.Count == 0)
            {
                return NotFound("No levels of experience");
            }



            return Ok(experienceLevels);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetExperienceLevelById(int id)
        {
            var experienceLevel = _mapper.Map<ExperienceLevelDTO>(await _experienceLevelRepository.GetExperienceLevelByIdAsync(id));

            try
            {

                if (experienceLevel == null)
                {
                    return NotFound("No experience level");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(experienceLevel);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddExperienceLevel(ExperienceLevelDTO experienceLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var experienceLevelMap = _mapper.Map<ExperienceLevel>(experienceLevel);
            var experienceLevelCreated = await _experienceLevelRepository.AddExperienceLevelAsync(experienceLevelMap);

            if (!await experienceLevelCreated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in creating");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExperienceLevel(int id, ExperienceLevelDTO experienceLevel)
        {
            if (id != experienceLevel.ExperienceLevelId)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var experienceLevelMap = _mapper.Map<ExperienceLevel>(experienceLevel);
            var experienceLevelUpdated = _experienceLevelRepository.UpdateExperienceLevelAsync(experienceLevelMap);

            if (!await experienceLevelUpdated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperienceLevel(int id)
        {
            var experienceLevel = await _experienceLevelRepository.GetExperienceLevelByIdAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _experienceLevelRepository.DeleteExperienceLevelAsync(experienceLevel))
            {
                ModelState.AddModelError("", "Error");
            }
            return Ok("Succ");

        }
    }
}
