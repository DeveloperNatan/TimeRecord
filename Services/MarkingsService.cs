using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeRecord.Data;
using TimeRecord.Models;
using TimeRecord.Validation;

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
            var time = new Marking
            {
                MatriculaId = marking.MatriculaId,
                Timestamp = DateTime.UtcNow,
                MarkingType = marking.MarkingType,
            };

            MarkingValidator.Validate(time);
            _appdbcontext.Markings.Add(time);
            await _appdbcontext.SaveChangesAsync();
            return time;
        }

        public async Task<IEnumerable<Marking>> Find()
        {
            var allTime = await _appdbcontext.Markings.ToListAsync();
            if (allTime == null)
            {
                throw new ValidationException("Nenhuma marcação encontrada!");
            }
            return allTime;
        }

        public async Task<Marking> FindOne(int id)
        {
            var time = await _appdbcontext.Markings.FindAsync(id);
            if (time == null)
            {
                throw new ValidationException("Nenhuma marcação encontrada!");
            }
            return time;
        }

        public async Task<Marking> DeleteOne(int id)
        {
            var time = await _appdbcontext.Markings.FindAsync(id);
            if (time == null)
            {
                throw new ValidationException("Marcação não existe!");
            }
            _appdbcontext.Remove(time);
            await _appdbcontext.SaveChangesAsync();
            return time;
        }

        public async Task<Marking> UpdateOne(Marking marking, int id)
        {
            var time = await _appdbcontext.Markings.FindAsync(id);
            if (time == null)
            {
                throw new ValidationException("Não é possivel atualizar marcação inexistente!");
            }

            await _appdbcontext.SaveChangesAsync();
            return time;
        }
    }
}
