using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TimeRecord.Data;
using TimeRecord.DTO.Company;
using TimeRecord.Models;
using TimeRecord.Validation;

namespace TimeRecord.Services
{
    public class BusinessService(AppDbContext appDbContext)
    {
        public async Task<IEnumerable<CompanyResponseDto>> GetUserAsync()
        {
            var companies = await appDbContext.Company.ToListAsync();
            if (!companies.Any())
            {
                throw new ValidationException("No registry found");
            }

            var response = companies.Select(company => new CompanyResponseDto()
            {
                Id = company.Id,
                Name = company.Name,
                IsActive = company.IsActive,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt,
            });
            return response;
        }

        public async Task<CompanyResponseDto> GetUserAsync(int id)
        {
            var company = await appDbContext.Company.FindAsync(id);
            if (company == null)
            {
                throw new ValidationException("Doesn't exist company");
            }

            var response = new CompanyResponseDto()
            {
                Id = company.Id,
                Name = company.Name,
                IsActive = company.IsActive,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt
            };
            return response;
        }

        public async Task<CompanyResponseDto> CreateCompanyAsync(CompanyCreateDto dataDto)
        {
            BusinessValidator.Validate(dataDto);
            var existingCompany = await appDbContext.Company.AnyAsync(c => c.Name == dataDto.Name);
            if (existingCompany)
            {
                throw new ValidationException("This name already exists, try another");
            }

            //save data
            var createdCompany = new Company
            {
                Name = dataDto.Name,
                IsActive = dataDto.IsActive,
                CreatedAt = DateTime.UtcNow,
            };
            await appDbContext.Company.AddAsync(createdCompany);
            await appDbContext.SaveChangesAsync();

            //just result 
            var response = new CompanyResponseDto()
            {
                Id = createdCompany.Id,
                Name = createdCompany.Name,
                IsActive = createdCompany.IsActive,
                CreatedAt = createdCompany.CreatedAt,
            };

            return response;
        }

        public async Task<CompanyResponseDto> UpdateCompanyAsync(CompanyCreateDto dataDto, int id)
        {
            var updatedCompany = await appDbContext.Company.FindAsync(id);
            if (updatedCompany == null)
            {
                throw new ValidationException("Doesn't exist company");
            }
            if (dataDto == null)
            {
                throw new ValidationException("Invalid data");
            }
            
       
            updatedCompany.Name = dataDto.Name;
            updatedCompany.IsActive = dataDto.IsActive;
            updatedCompany.UpdatedAt = DateTime.UtcNow;

         
            await appDbContext.SaveChangesAsync();

            var response = new CompanyResponseDto()
            {
                Id = updatedCompany.Id,
                Name = updatedCompany.Name,
                IsActive = updatedCompany.IsActive,
                CreatedAt =  updatedCompany.CreatedAt,
                UpdatedAt = updatedCompany.UpdatedAt,
            };
            return response;
        }

        public async Task<CompanyMessageDto> DeleteCompanyAsync(int id)
        {
            var deletedCompany = await appDbContext.Company.FindAsync(id);
            if (deletedCompany == null)
            {
                throw new ValidationException("Doesn't exist company");
            }
            appDbContext.Remove(deletedCompany);
            await appDbContext.SaveChangesAsync();

            return new CompanyMessageDto()
            {
                Message = $"Company {deletedCompany.Name} was deleted!"
            };
        }
    }
}