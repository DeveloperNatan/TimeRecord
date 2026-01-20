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
        public async Task<MarkingsResponseDto> CreateMarkingAsync(MarkingsCreateDto dataDto)
        {
            var employee = await appDbContext.Employees.FindAsync(dataDto.RegistrationId);
            if (employee == null)
            {
                throw new KeyNotFoundException("Employee ID not found in the system!");
            }

            //save data
            var marking = new Marking()
            {
                RegistrationId = dataDto.RegistrationId,
                Timestamp = DateTime.UtcNow,
            };

            MarkingValidator.Validate(marking);
            appDbContext.Markings.Add(marking);
            await appDbContext.SaveChangesAsync();

            var response = new MarkingsResponseDto()
            {
                RegistrationId = marking.RegistrationId,
                Timestamp = marking.Timestamp.ToString("dd/MM/yyyy HH:mm"),
            };
            
            return response;
        }

        public async Task<IEnumerable<MarkingsResponseDto>> GetAllMarkingsAsync()
        {
            var markings = await appDbContext.Markings.ToListAsync();
            if (!markings.Any())
            {
                throw new ValidationException("No time markings found!");
            }

            var response = markings.Select(marking => new MarkingsResponseDto()
            {
                PontoId = marking.PontoId,
                RegistrationId = marking.RegistrationId,
                Timestamp = marking.Timestamp.ToString("dd/MM/yyyy HH:mm"),
            });
            return response;
        }

        public async Task<MarkingsResponseDto> GetMarkingsAsync(int id)
        {
            var marking = await appDbContext.Markings.FindAsync(id);
            if (marking == null)
            {
                throw new ValidationException("No time markings found for this employee!");
            }

            var response = new MarkingsResponseDto()
            {
                PontoId = marking.PontoId,
                RegistrationId = marking.RegistrationId,
                Timestamp = marking.Timestamp.ToString("dd/MM/yyyy HH:mm"),
            };
            return response;
        }

        public async Task<MarkingMessageDto> DeleteMarkingAsync(int id)
        {
            var deletedMarking = await appDbContext.Markings.FindAsync(id);
            if (deletedMarking == null)
            {
                throw new ValidationException("There is no marking!");
            }

            appDbContext.Remove(deletedMarking);
            await appDbContext.SaveChangesAsync();

            var response = new MarkingMessageDto()
            {
                Message =
                    $"The time markings of Employee ID {deletedMarking.RegistrationId} of date {deletedMarking.Timestamp} has been successfully deleted."
            };
            return response;
        }

        public async Task<MarkingsResponseDto> UpdateMarkingAsync(int id)
        {
            var updatedMarking = await appDbContext.Markings.FindAsync(id);
            if (updatedMarking == null)
            {
                throw new ValidationException("It's not possible to update a non-existing time!");
            }
            
            updatedMarking.Timestamp = DateTime.UtcNow;
            await appDbContext.SaveChangesAsync();
            var response = new MarkingsResponseDto()
            {
                PontoId = updatedMarking.PontoId,
                RegistrationId = updatedMarking.RegistrationId,
                Timestamp = updatedMarking.Timestamp.ToString("dd/MM/yyyy HH:mm"),
            };
            return response;
        }
    }
}