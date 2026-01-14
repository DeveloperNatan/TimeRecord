using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TimeRecord.Data;
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


        public async Task<IEnumerable<Business>> FindAll()
        {
            var allUser = await _appdbcontext.Business.ToListAsync();
            if (allUser == null)
            {
                throw new ValidationException("Nenhum registro foi encontrado");
            }

            return allUser;
        }
    }
}