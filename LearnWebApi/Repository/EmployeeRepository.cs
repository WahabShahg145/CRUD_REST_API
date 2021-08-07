using DataAcessLayer;
using LearnWebApi.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnWebApi.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context = null;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result =  await _context.Employee.AddAsync(employee);
          await  _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            var result = await _context.Employee.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (result != null)
            {
                _context.Employee.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Employee> GetEmployee(int Id)
        {
            var result = await _context.Employee.FirstOrDefaultAsync(a=>a.Id==Id);
            return result;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var result = await _context.Employee.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Employee>> Search(string name)
        {
            IQueryable<Employee> query = _context.Employee;
            if (!string.IsNullOrEmpty(name))
            {
                query=  query.Where(x=>x.Name.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var result = await _context.Employee.FirstOrDefaultAsync(a => a.Id == employee.Id);
            if (result != null)
            {
                result.Name = employee.Name;
                result.City = employee.City;
                result.Salary = employee.Salary;
                await _context.SaveChangesAsync();

                return result;
            }
            return null;
        }
    }
}
