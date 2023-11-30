using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Repositories
{
    public class CompensationRespository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRespository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            compensation.compensationId = Guid.NewGuid().ToString();
            _employeeContext.Compensations.Add(compensation);
            return compensation;
        }

        public Compensation GetById(string id)
        {
            var compensation =  _employeeContext.Compensations.SingleOrDefault(e => e.compensationId == id);

            // Cause things to populate
            var thing = _employeeContext.Compensations.ToList();

            return compensation; 
        }

        public Compensation GetByEmployeeId(string id)
        {
            // Return the latest effective compensation for the given employee
            var compensation = _employeeContext.Compensations
                .Where(c => c.employeeId == id)
                .OrderByDescending(x => x.effectiveDate)
                .FirstOrDefault();

            return compensation; 
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Compensation Remove(Compensation compensation)
        {
            return _employeeContext.Remove(compensation).Entity;
        }
    }
}
