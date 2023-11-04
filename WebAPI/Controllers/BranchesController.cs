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
    public class BranchesController : ControllerBase
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        public BranchesController(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBranches()
        {

            var branches = _mapper.Map<List<BranchDTO>>(await _branchRepository.GetBranchesAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (branches.Count == 0)
            {
                return NotFound("No branches");
            }



            return Ok(branches);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchById(int id)
        {
            var branch = _mapper.Map<BranchDTO>(await _branchRepository.GetBranchByIdAsync(id));

            try
            {

                if (branch == null)
                {
                    return NotFound("No branch");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(branch);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddBranch(BranchDTO branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branchMap = _mapper.Map<Branch>(branch);
            var branchCreated = await _branchRepository.AddBranchAsync(branchMap);

            if (!await branchCreated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in creating");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(int id, BranchDTO branch)
        {
            if (id != branch.BranchId)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var branchMap = _mapper.Map<Branch>(branch);
            var branchUpdated = _branchRepository.UpdateBranchAsync(branchMap);

            if (!await branchUpdated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }
            return Ok("Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            var branch = await _branchRepository.GetBranchByIdAsync(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _branchRepository.DeleteBranchAsync(branch))
            {
                ModelState.AddModelError("", "Error");
            }
            return Ok("Succ");

        }
    }
}
