using System;
using System.Collections.Generic;
using System.Linq;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public ReportingStructure GetByEmployeeId(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                Employee employee = _employeeRepository.GetById(id);
                if(employee != null)
                {
                    // "The number of reports is determined to be the number of directReports for an
                    // employee and all of their direct reports."
                    // Total up the number of reports recursively
                    int reportCount = 0;
                    if (employee.DirectReports != null)
                    {
                        List<Employee> reports = employee.DirectReports.ToList();
                        reportCount = reports.Count;
                        foreach (Employee report in reports)
                        {
                            if (report.DirectReports != null)
                            {
                                ReportingStructure reportingStructure = GetByEmployeeId(report.EmployeeId);
                                if (reportingStructure is not null)
                                {
                                    reportCount += reportingStructure.numberOfReports;
                                }
                            }
                        }
                    }
                    return new ReportingStructure(employee, reportCount);
                }
                return null;
            }
            return null;
        }
    }
}
