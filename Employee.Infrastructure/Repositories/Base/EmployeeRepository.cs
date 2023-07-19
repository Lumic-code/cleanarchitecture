using Employee.Core.Repositories;
using Employee.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Repositories.Base
{
    public class EmployeeRepository : Repository<Employee.Core.Entities.Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeContext employeeContext) : base(employeeContext)
        {}

        public async Task<IEnumerable<Core.Entities.Employee>> GetEmployeeByFirstName(string firstName)
        {
                return await _employeeContext.Employees.Where(m => m.LastName == firstName).ToListAsync();
        }

        public async Task<IEnumerable<Core.Entities.Employee>> GetEmployeeByLastName(string lastName)
        {
            return await _employeeContext.Employees.Where(m => m.LastName == lastName).ToListAsync();
        }
    }
}
