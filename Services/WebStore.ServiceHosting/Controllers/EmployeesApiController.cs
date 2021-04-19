using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[employees]")]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;

        public EmployeesApiController(IEmployeesData EmployeesData)
        {
            _EmployeesData = EmployeesData;
        }

        [HttpGet] // https://localhost:5001/api/employees
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        [HttpGet("{id}")] // https://localhost:5001/api/employees/5
        public Employee Get(int id) => _EmployeesData.Get(id);

        [HttpGet("employee")] // https://localhost:5001/api/employees/employee?LastName=Иванов&FirstName=Иван&Patronymic=Иванович
        public Employee GetByName(string LastName, string FirstName, string Patronymic) =>
           _EmployeesData.GetByName(LastName, FirstName, Patronymic);

        [HttpPost]
        public int Add(Employee employee) => _EmployeesData.Add(employee);

        [HttpPost("employee")] // post -> https://localhost:5001/api/employees/employee?LastName=Иванов&FirstName=Иван&Patronymic=Иванович&Age=37
        public Employee Add(string LastName, string FirstName, string Patronymic, int Age) =>
            _EmployeesData.Add(LastName, FirstName, Patronymic, Age);

        [HttpPut] // put -> http://localhost:5001/api/employees/5
        public void Update(Employee employee)
        {
            _EmployeesData.Update(employee);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _EmployeesData.Delete(id);
        }
    }
}
