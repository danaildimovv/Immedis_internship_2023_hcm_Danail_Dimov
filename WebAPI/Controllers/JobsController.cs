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
    public class JobsController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;
        public JobsController(IJobRepository jobRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {

            var jobs = _mapper.Map<List<JobDTO>>(await _jobRepository.GetJobsAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (jobs.Count == 0)
            {
                return NotFound("No jobs");
            }



            return Ok(jobs);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = _mapper.Map<JobDTO>(await _jobRepository.GetJobByIdAsync(id));

            try
            {

                if (job == null)
                {
                    return NotFound("No job");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(job);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateJob(JobDTO job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobMap = _mapper.Map<Job>(job);
            var jobCreated = await _jobRepository.CreateJobAsync(jobMap);

            if (!await jobCreated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in creating");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, JobDTO job)
        {
            if (id != job.JobId)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var jobMap = _mapper.Map<Job>(job);
            var jobUpdated = _jobRepository.UpdateJobAsync(jobMap);

            if (!await jobUpdated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _jobRepository.GetJobByIdAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _jobRepository.DeleteJobAsync(job))
            {
                ModelState.AddModelError("", "Error");
            }
            return Ok("Succ");

        }

    }
}
