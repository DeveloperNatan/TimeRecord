using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TimeRecord.Data;
using TimeRecord.DTO.Markings;
using TimeRecord.Exceptions;
using TimeRecord.Models;
using TimeRecord.Validation;

namespace TimeRecord.Services
{
    public class MarkingsService(AppDbContext appDbContext)
    {
        public async Task<MarkingsResponseDto> CreateMarkingAsync(MarkingsCreateDto dataDto)
        {
            var employee = await appDbContext.Employees.FindAsync(dataDto.UsedId);
            if (employee == null)
            {
                throw new NotFoundException(404, "Employee ID not found in the system!");
            }

            //save data
            var marking = new Marking()
            {
                UserId = dataDto.UsedId,
                Timestamp = DateTime.UtcNow,
            };

            MarkingValidator.Validate(marking);
            appDbContext.Markings.Add(marking);
            await appDbContext.SaveChangesAsync();

            var response = new MarkingsResponseDto()
            {
                Id = marking.Id,
                UserId = marking.UserId,
                Timestamp = marking.Timestamp,
            };
            
            return response;
        }

        public async Task<IEnumerable<MarkingsResponseDto>> GetAllMarkingsAsync()
        {
            var markings = await appDbContext.Markings.ToListAsync();
           

            var response = markings.Select(marking => new MarkingsResponseDto()
            {
                Id = marking.Id,
                UserId = marking.UserId,
                Timestamp = marking.Timestamp,
            });
            return response;
        }

        public async Task<MarkingsResponseDto> GetMarkingsAsync(int id)
        {
            var marking = await appDbContext.Markings.FindAsync(id);
            if (marking == null)
            {
                throw new NotFoundException(404, "No time markings found for this employee!");
            }

            var response = new MarkingsResponseDto()
            {
                Id = marking.Id,
                UserId = marking.UserId,
                Timestamp = marking.Timestamp,
            };
            return response;
        }

        public async Task<MarkingMessageDto> DeleteMarkingAsync(int id)
        {
            var deletedMarking = await appDbContext.Markings.FindAsync(id);
            if (deletedMarking == null)
            {
                throw new NotFoundException(404, "There is no markings");
            }

            appDbContext.Remove(deletedMarking);
            await appDbContext.SaveChangesAsync();

            var response = new MarkingMessageDto()
            {
                StatusCode = 200,
                UserId = deletedMarking.UserId,
                Message =
                    $"The time markings of date {deletedMarking.Timestamp} has been successfully deleted."
            };
            return response;
        }

        public async Task<MarkingsResponseDto> UpdateMarkingAsync(int id)
        {
            var updatedMarking = await appDbContext.Markings.FindAsync(id);
            if (updatedMarking == null)
            {
                throw new NotFoundException(404, "It's not possible to update a non-existing time!");
            }
            
            updatedMarking.Timestamp = DateTime.UtcNow;
            await appDbContext.SaveChangesAsync();
            var response = new MarkingsResponseDto()
            {
                Id = updatedMarking.Id,
                UserId = updatedMarking.UserId,
                Timestamp = updatedMarking.Timestamp,
            };
            return response;
        }
    }
}