using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TimeRecord.Data;
using TimeRecord.DTO.Company;
using TimeRecord.Models;

namespace TimeRecord.Services
{
    public class BusinessService
    {
        private readonly AppDbContext _appdbcontext;

        public BusinessService(AppDbContext appdbcontext)
        {
            _appdbcontext = appdbcontext;
        }


        public async Task<IEnumerable<Business>> GetAllUsersAsync()
        {
            var allUser = await _appdbcontext.Business.ToListAsync();
            if (!allUser.Any())
            {
                throw new ValidationException("No registry found");
            }

            return allUser;
        }

        public async Task<CompanyResponseDTO> CreateCompanyAsync(CompanyCreateDTO dto)
        {
            if (dto == null)                                     
            {                                                         
                throw new ValidationException("Invalid data");        
            }
            
            //save data
            var business = new Business
            {
                Name = dto.Name,
                IsActive = dto.IsActive,
                CreatedAt = DateTime.UtcNow,
            };
             await _appdbcontext.Business.AddAsync(business);
             await _appdbcontext.SaveChangesAsync();
             
             //just result 
             var response = new CompanyResponseDTO()
             {
                 Id = business.Id,
                 Name = business.Name,
                 IsActive = business.IsActive,
                 CreatedAt = business.CreatedAt,
             };
             
             return response;
        }
    }
}