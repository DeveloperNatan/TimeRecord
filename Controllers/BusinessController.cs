using System.ComponentModel.DataAnnotations;
using TimeRecord.Models;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.DTO.Company;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController(BusinessService businessService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var companies = await businessService.GetUserAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var company = await businessService.GetUserAsync(id);
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CompanyCreateDTO dto)
        {
            var companyCreated = await businessService.CreateCompanyAsync(dto);
            return Ok(companyCreated);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(CompanyCreateDTO dto, int id)
        {
            var updatedCompany = await businessService.UpdateCompanyAsync(dto, id);
            return Ok(updatedCompany);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedCompany = await businessService.DeleteCompanyAsync(id);
            return Ok(deletedCompany);
        }
    }
}