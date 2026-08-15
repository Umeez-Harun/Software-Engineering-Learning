using ServiceContracts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IEmployeeService
    {
        public Task<EmployeeResponse> AddEmployee(EmployeeRequest request);
        public Task<bool> DeleteEmployee(Guid employeeID);
        public Task<List<EmployeeResponse>> getAllEmployees();

    }
}
