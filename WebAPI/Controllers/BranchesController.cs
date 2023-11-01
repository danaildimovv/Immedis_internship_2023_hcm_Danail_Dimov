using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTO;
using WebAPI.Interfaces;

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
    }
}
