using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;

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
    }
}
