using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppointmentScheduler.Domain;
using AppointmentScheduler.Data;

namespace AppointmentScheduler.Logic
{
    public class ReportHandler
    {
        private readonly AppointmentRepository _repository;

        public ReportHandler(AppointmentRepository repository)
        {
            _repository = repository;
        }
        public List<AppointTypeByMonthReport> GenerateAppointmentsByMonthReport()
        {
            var appointments = _repository.GetAll();

            return [.. appointments
                .GroupBy(a => new { a.Start.Year, a.Start.Month, a.Type })
                .Select(g => new AppointTypeByMonthReport
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Type = g.Key.Type,
                    Count = g.Count()
                })];
        }

        public List<ScheduleReport> GenerateUserScheduleReport()
        {
            var appointments = _repository.GetAll();

            return [.. appointments
                .OrderBy(a => a.UserId)
                .ThenBy(a => a.Start)
                .Select(a => new ScheduleReport
                {
                    Id = a.UserId,
                    Type = a.Type,
                    Start = a.Start,
                    End = a.End
                })];
        }

        public List<ScheduleReport> GenerateCustomerScheduleReport(int customerId)
        {
            var appointments = _repository.GetAll();

            return [.. appointments
                .Where(a => a.CustomerId == customerId)
                .OrderBy(a => a.Start)
                .Select(a => new ScheduleReport
                {
                    Id = customerId,
                    Type = a.Type,
                    Start = a.Start,
                    End = a.End
                })];
        }
    }
}
