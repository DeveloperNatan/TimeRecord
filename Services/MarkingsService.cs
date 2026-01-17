using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TimeRecord.Data;
using TimeRecord.DTO.Markings;
using TimeRecord.Models;
using TimeRecord.Validation;

namespace TimeRecord.Services
{
    public class MarkingsService(AppDbContext appDbContext)
    {
        public async Task<MarkingsResponseDTO> CreateMarkingAsync(MarkingsCreateDTO dto)
        {
            var employee = await appDbContext.Employees.FindAsync(dto.MatriculaId);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            //save data
            var marking = new Marking
            {
                MatriculaId = dto.MatriculaId,
                Timestamp = DateTime.UtcNow,
            };

            MarkingValidator.Validate(marking);
            appDbContext.Markings.Add(marking);
            await appDbContext.SaveChangesAsync();

            var response = new MarkingsResponseDTO()
            {
                PontoId = marking.PontoId,
                MatriculaId = marking.MatriculaId,
                Timestamp = marking.Timestamp,
            };
            
            return response;
        }

        public async Task<IEnumerable<MarkingsResponseDTO>> GetAllMarkingsAsync()
        {
            var markings = await appDbContext.Markings.ToListAsync();
            if (!markings.Any())
            {
                throw new ValidationException("No time markings found!");
            }

            var response = markings.Select(time => new MarkingsResponseDTO()
            {
                PontoId = time.PontoId,
                MatriculaId = time.MatriculaId,
                Timestamp = time.Timestamp,
            });
            return response;
        }

        public async Task<MarkingsResponseDTO> GetMarkingsAsync(int id)
        {
            var marking = await appDbContext.Markings.FindAsync(id);
            if (marking == null)
            {
                throw new ValidationException("No time markings found for this employee!");
            }

            var response = new MarkingsResponseDTO()
            {
                PontoId = marking.PontoId,
                MatriculaId = marking.MatriculaId,
                Timestamp = marking.Timestamp,
            };
            return response;
        }

        public async Task<MarkingMessageDTO> DeleteMarkingAsync(int id)
        {
            var deletedMarking = await appDbContext.Markings.FindAsync(id);
            if (deletedMarking == null)
            {
                throw new ValidationException("There is no marking!");
            }

            appDbContext.Remove(deletedMarking);
            await appDbContext.SaveChangesAsync();

            var response = new MarkingMessageDTO()
            {
                Message =
                    $"The time markings of Employee ID {deletedMarking.MatriculaId} of date {deletedMarking.Timestamp} has been successfully deleted."
            };
            return response;
        }

        public async Task<MarkingsResponseDTO> UpdateMarkingAsync(int id)
        {
            var updatedMarking = await appDbContext.Markings.FindAsync(id);
            if (updatedMarking == null)
            {
                throw new ValidationException("It's not possible to update a non-existing time!");
            }
            
            updatedMarking.Timestamp = DateTime.UtcNow;
            await appDbContext.SaveChangesAsync();
            var response = new MarkingsResponseDTO()
            {
                PontoId = updatedMarking.PontoId,
                MatriculaId = updatedMarking.MatriculaId,
                Timestamp = updatedMarking.Timestamp
            };
            return response;
        }
    }
}