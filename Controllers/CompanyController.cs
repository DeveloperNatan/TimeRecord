using Microsoft.AspNetCore.Mvc;
using TimeRecord.DTO.Company;
using TimeRecord.Services;

namespace TimeRecord.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController(CompanyService companyService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var companies = await companyService.GetUserAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var company = await companyService.GetUserAsync(id);
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CompanyCreateDto createRequestDto)
        {
            var companyCreated = await companyService.CreateCompanyAsync(createRequestDto);
            return Ok(companyCreated);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(CompanyCreateDto createRequestDto, int id)
        {
            var updatedCompany = await companyService.UpdateCompanyAsync(createRequestDto, id);
            return Ok(updatedCompany);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var deletedCompany = await companyService.DeleteCompanyAsync(id);
            return Ok(deletedCompany);
        }
    }
}