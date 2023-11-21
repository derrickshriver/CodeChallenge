using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        public String compensationId { get; set; }
        public String employeeId { get; set; }
        public decimal salary { get; set; }
        public DateTime effectiveDate { get; set; }
            }
}
