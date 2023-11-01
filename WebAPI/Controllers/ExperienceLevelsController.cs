﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;

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
    }
}