using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using TimeRecord.Data;
using TimeRecord.DTO.Markings;
using TimeRecord.Exceptions;
using TimeRecord.Models;
using TimeRecord.Validation;

namespace TimeRecord.Services
{
    public class TimeRecordsService(AppDbContext appDbContext)
    {
        public async Task<TimeRecordsResponseDto> CreateMarkingAsync(TimeRecordsCreateDto dataDto)
        {
            Console.WriteLine($"Matriculation recebida: {dataDto.Matriculation}");
            var employee = await appDbContext.Employees
                .FirstOrDefaultAsync(e => e.Matriculation == dataDto.Matriculation);
            Console.WriteLine(employee);
            if (employee == null)
            {
                throw new NotFoundException(404, "Matriculation not found in the system!");
            }

            //save data
            var marking = new TimeRecords()
            {
                EmployeeId = employee.Id,
                CompanyId = employee.CompanyId,
                RecordedAt = DateTime.UtcNow,
            };

            MarkingValidator.Validate(marking);
            appDbContext.TimeRecords.Add(marking);
            await appDbContext.SaveChangesAsync();

            var response = new TimeRecordsResponseDto()
            {
                Id = marking.Id,
                UserId = marking.EmployeeId,
                RecordedAt = marking.RecordedAt,
            };
            
            return response;
        }

        public async Task<IEnumerable<TimeRecordsResponseDto>> GetAllMarkingsAsync()
        {
            var markings = await appDbContext.TimeRecords.ToListAsync();
           

            var response = markings.Select(marking => new TimeRecordsResponseDto()
            {
                Id = marking.Id,
                UserId = marking.EmployeeId,
                RecordedAt = marking.RecordedAt,
            });
            return response;
        }

        public async Task<TimeRecordsResponseDto> GetMarkingsAsync(int id)
        {
            var marking = await appDbContext.TimeRecords.FindAsync(id);
            if (marking == null)
            {
                throw new NotFoundException(404, "No time markings found for this employee!");
            }

            var response = new TimeRecordsResponseDto()
            {
                Id = marking.Id,
                UserId = marking.EmployeeId,
                RecordedAt = marking.RecordedAt,
            };
            return response;
        }

        public async Task<TimeRecordsMessageDto> DeleteMarkingAsync(int id)
        {
            var deletedMarking = await appDbContext.TimeRecords.FindAsync(id);
            if (deletedMarking == null)
            {
                throw new NotFoundException(404, "There is no markings");
            }

            appDbContext.Remove(deletedMarking);
            await appDbContext.SaveChangesAsync();

            var response = new TimeRecordsMessageDto()
            {
                StatusCode = 200,
                UserId = deletedMarking.EmployeeId,
                Message =
                    $"The time markings of date {deletedMarking.RecordedAt} has been successfully deleted."
            };
            return response;
        }

        public async Task<TimeRecordsResponseDto> UpdateMarkingAsync(int id)
        {
            var updatedMarking = await appDbContext.TimeRecords.FindAsync(id);
            if (updatedMarking == null)
            {
                throw new NotFoundException(404, "It's not possible to update a non-existing time!");
            }
            
            updatedMarking.RecordedAt = DateTime.UtcNow;
            await appDbContext.SaveChangesAsync();
            var response = new TimeRecordsResponseDto()
            {
                Id = updatedMarking.Id,
                UserId = updatedMarking.EmployeeId,
                RecordedAt = updatedMarking.RecordedAt,
            };
            return response;
        }
    }
}