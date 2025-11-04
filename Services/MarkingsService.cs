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

        public async Task<Marking> Post(Marking req)
        {
            var marking = new Marking
            {
                MatriculaId = req.MatriculaId,
                Timestamp = DateTime.UtcNow,
                MarkingType = req.MarkingType,
            };
            var CreateTime = _appdbcontext.Markings.Add(marking);
            try
            {
                await _appdbcontext.SaveChangesAsync();
                return CreateTime.Entity;
            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }

        public async Task<IEnumerable<Marking>> Find()
        {
            var FindAllTime = await _appdbcontext.Markings.ToListAsync();
            try
            {
                return FindAllTime;
            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }

        public async Task<Marking> FindOne(int id)
        {
            var FindTime = await _appdbcontext.Markings.FindAsync(id);
            try
            {
                return FindTime;
            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }

        public async Task<Marking> DeleteOne(int id)
        {
            var DeleteTime = await _appdbcontext.Markings.FindAsync(id);
            try
            {
                _appdbcontext.Remove(DeleteTime);
                await _appdbcontext.SaveChangesAsync();
                return DeleteTime;
            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }

        public async Task<Marking> UpdateOne(Marking marking, int id)
        {
            var UpdateTime = await _appdbcontext.Markings.FindAsync(id);
            try
            {
                UpdateTime.Timestamp = marking.Timestamp;
                UpdateTime.MarkingType = marking.MarkingType;
                await _appdbcontext.SaveChangesAsync();
                return UpdateTime;
            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }
    }
}
