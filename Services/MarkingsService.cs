using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeRecord.Data;
using TimeRecord.Models;

namespace TimeRecord.Services
{
    public class MarkingsService
    {
        private readonly AppDbContext _appdbcontext;

        public MarkingsService(AppDbContext appcontext)
        {
            _appdbcontext = appcontext;
        }

        public async Task<Marking> Post(Marking marking)
        {
            var CreateTime = _appdbcontext.Markings.Add(marking);
            await _appdbcontext.SaveChangesAsync();
            return CreateTime.Entity;
        }

        public async Task<IEnumerable<Marking>> Find()
        {
            var FindAllTime = await _appdbcontext.Markings.ToListAsync();
            return FindAllTime;
        }

        public async Task<Marking> FindOne(int id)
        {
            var FindTime = await _appdbcontext.Markings.FindAsync(id);
            return FindTime;
        }

        public async Task<Marking> Delete(int id)
        {
            var DeleteTime = await _appdbcontext.Markings.FindAsync(id);
            _appdbcontext.Remove(DeleteTime);
            await _appdbcontext.SaveChangesAsync();
            return DeleteTime;
        }
    }
}
