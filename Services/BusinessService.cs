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
            var createdCompany = new Company
            {
                Name = dto.Name,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
            };
            await appDbContext.Company.AddAsync(createdCompany);
            await appDbContext.SaveChangesAsync();

            //just result 
            var response = new CompanyResponseDTO()
            {
                Id = createdCompany.Id,
                Name = createdCompany.Name,
                IsActive = createdCompany.IsActive,
                CreatedAt = createdCompany.CreatedAt,
            };

            return response;
        }

        public async Task<CompanyResponseDTO> UpdateCompanyAsync(CompanyCreateDTO dto, int id)
        {
            var updatedCompany = await appDbContext.Company.FindAsync(id);
            if (updatedCompany == null)
            {
                throw new ValidationException("Doesn't exist company");
            }
            if (dto == null)
            {
                throw new ValidationException("Invalid data");
            }
            
       
            updatedCompany.Name = dto.Name;
            updatedCompany.IsActive = dto.IsActive;
            updatedCompany.UpdatedAt = DateTime.UtcNow;

         
            await appDbContext.SaveChangesAsync();

            var response = new CompanyResponseDTO()
            {
                Id = updatedCompany.Id,
                Name = updatedCompany.Name,
                IsActive = updatedCompany.IsActive,
                CreatedAt =  updatedCompany.CreatedAt,
                UpdatedAt = updatedCompany.UpdatedAt,
            };
            return response;
        }

        public async Task<CompanyMessageDTO> DeleteCompanyAsync(int id)
        {
            var deletedCompany = await appDbContext.Company.FindAsync(id);
            if (deletedCompany == null)
            {
                throw new ValidationException("Doesn't exist company");
            }
            appDbContext.Remove(deletedCompany);
            await appDbContext.SaveChangesAsync();

            return new CompanyMessageDTO()
            {
                Message = $"Company {deletedCompany.Name} was deleted!"
            };
        }
    }
}