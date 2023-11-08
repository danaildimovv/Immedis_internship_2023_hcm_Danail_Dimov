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
    public class EducationLevelsController : ControllerBase
    {
        private readonly IEducationLevelRepository _educationLevelRepository;
        private readonly IMapper _mapper;
        public EducationLevelsController(IEducationLevelRepository educationLevelRepository, IMapper mapper)
        {
            _educationLevelRepository = educationLevelRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEducationLevels()
        {

            var educationLevels = _mapper.Map<List<EducationLevelDTO>>(await _educationLevelRepository.GetEducationLevelsAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (educationLevels.Count == 0)
            {
                return NotFound("No levels of education");
            }



            return Ok(educationLevels);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEducationLevelById(int id)
        {
            var educationLevel = _mapper.Map<EducationLevelDTO>(await _educationLevelRepository.GetEducationLevelByIdAsync(id));

            try
            {

                if (educationLevel == null)
                {
                    return NotFound("No education level");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(educationLevel);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]

        public async Task<IActionResult> AddEducationLevel(EducationLevelDTO educationLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var educationLevelMap = _mapper.Map<EducationLevel>(educationLevel);
            var educationLevelCreated = await _educationLevelRepository.AddEducationLevelAsync(educationLevelMap);

            if (!await educationLevelCreated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in creating");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateEducationLevel(int id, EducationLevelDTO educationLevel)
        {
            if (id != educationLevel.EducationLevelId)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var educationLevelMap = _mapper.Map<EducationLevel>(educationLevel);
            var educationLevelUpdated = _educationLevelRepository.UpdateEducationLevelAsync(educationLevelMap);

            if (!await educationLevelUpdated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEducationLevel(int id)
        {
            var educationLevel = await _educationLevelRepository.GetEducationLevelByIdAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _educationLevelRepository.DeleteEducationLevelAsync(educationLevel))
            {
                ModelState.AddModelError("", "Error");
            }
            return Ok("Succ");

        }
    }
    
}
