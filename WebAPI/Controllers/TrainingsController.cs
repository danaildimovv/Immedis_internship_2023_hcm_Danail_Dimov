﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingsController : ControllerBase
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly IMapper _mapper;
        public TrainingsController(ITrainingRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrainings()
        {

            var trainings = _mapper.Map<List<TrainingDTO>>(await _trainingRepository.GetTrainingsAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (trainings.Count == 0)
            {
                return NotFound("No trainings");
            }



            return Ok(trainings);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingById(int id)
        {
            var training = _mapper.Map<TrainingDTO>(await _trainingRepository.GetTrainingByIdAsync(id));

            try
            {

                if (training == null)
                {
                    return NotFound("No training");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(training);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
