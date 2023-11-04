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
    public class PayrollsController : ControllerBase
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IMapper _mapper;
        public PayrollsController(IPayrollRepository payrollRepository, IMapper mapper)
        {
            _payrollRepository = payrollRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayrolls()
        {

            var payrolls = _mapper.Map<List<PayrollDTO>>(await _payrollRepository.GetPayrollsAsync());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (payrolls.Count == 0)
            {
                return NotFound("No payrolls");
            }



            return Ok(payrolls);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayrollById(int id)
        {
            var payroll = _mapper.Map<PayrollDTO>(await _payrollRepository.GetPayrollByIdAsync(id));

            try
            {

                if (payroll == null)
                {
                    return NotFound("No payroll");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(payroll);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddPayroll(PayrollDTO payroll)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payrollMap = _mapper.Map<Payroll>(payroll);
            var payrollCreated = await _payrollRepository.AddPayrollAsync(payrollMap);

            if (!await payrollCreated)
            {
                ModelState.AddModelError("", "Something Went Wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Success in creating");
        }
    }
}
