using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<EmployeeResponse> AddEmployee(EmployeeRequest request)
        {
            bool result =  InputValidator.validateInput(request);
            if (!result)
            {
                throw new ArgumentException("Please fill in all required fields to Add Employee");
            }
            Employee employee = request.convertToEmployee();
            employee.ApplicationUserId = request.userID;
            _dbContext.employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            return employee.convertToEmployeeResponse();
        }

        public async Task<bool> DeleteEmployee(Guid employeeID)
        {
            Employee? employee = await _dbContext.employees.FirstOrDefaultAsync(temp => temp.id == employeeID);
            if(employee == null)
            {
                throw new InvalidOperationException("Employee could not be found");
            }
            
            employee.isDeleted = true;
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<EmployeeResponse>> getAllEmployees()
        {
            return await _dbContext.employees.Where(temp => temp.isDeleted == false).Select(temp => temp.convertToEmployeeResponse()).ToListAsync();
        }
    }
}
