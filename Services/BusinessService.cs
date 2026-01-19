using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.Data;
using TimeRecord.DTO.Company;
using TimeRecord.Models;

namespace TimeRecord.Services
{
    public class BusinessService(AppDbContext appDbContext)
    {
        public async Task<IEnumerable<CompanyResponseDTO>> GetUserAsync()
        {
            var companies = await appDbContext.Company.ToListAsync();
            if (!companies.Any())
            {
                throw new ValidationException("No registry found");
            }

            var response = companies.Select(company => new CompanyResponseDTO()
            {
                Id = company.Id,
                Name = company.Name,
                IsActive = company.IsActive,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt,
            });
            return response;
        }

        public async Task<CompanyResponseDTO> GetUserAsync(int id)
        {
            var company = await appDbContext.Company.FindAsync(id);
            if (company == null)
            {
                throw new ValidationException("Doesn't exist company");
            }

            var response = new CompanyResponseDTO()
            {
                Id = company.Id,
                Name = company.Name,
                IsActive = company.IsActive,
                CreatedAt = company.CreatedAt,
                UpdatedAt = company.UpdatedAt
            };
            return response;
        }

        public async Task<CompanyResponseDTO> CreateCompanyAsync(CompanyCreateDTO dto)
        {
            if (dto == null)
            {
                throw new ValidationException("Invalid data");
            }

            //save data
            var company = new Company
            {
                Name = dto.Name,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
            };
            await appDbContext.Company.AddAsync(company);
            await appDbContext.SaveChangesAsync();

            //just result 
            var response = new CompanyResponseDTO()
            {
                Id = company.Id,
                Name = company.Name,
                IsActive = company.IsActive,
                CreatedAt = company.CreatedAt,
            };

            return response;
        }

        public async Task<CompanyResponseDTO> UpdateCompanyAsync(CompanyCreateDTO dto, int id)
        {
            var company = await appDbContext.Company.FindAsync(id);
            if (company == null)
            {
                throw new ValidationException("Doesn't exist company");
            }
            if (dto == null)
            {
                throw new ValidationException("Invalid data");
            }
            
       
            company.Name = dto.Name;
            company.IsActive = dto.IsActive;
            company.UpdatedAt = DateTime.UtcNow;

         
            await appDbContext.SaveChangesAsync();

            var response = new CompanyResponseDTO()
            {
                Id = company.Id,
                Name = company.Name,
                IsActive = company.IsActive,
                CreatedAt =  company.CreatedAt,
                UpdatedAt = company.UpdatedAt,
            };
            return response;
        }
    }
}