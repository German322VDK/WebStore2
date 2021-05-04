using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WebStore.Domain.Models;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebAPI.Employees)]
    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesApiController> _Logger;

        public EmployeesApiController(IEmployeesData EmployeesData, ILogger<EmployeesApiController> Logger)
        {
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        }

        [HttpGet] // https://localhost:5001/api/employees
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        [HttpGet("{id}")] // https://localhost:5001/api/employees/5
        public Employee Get(int id) => _EmployeesData.Get(id);

        [HttpGet("employee")] // https://localhost:5001/api/employees/employee?LastName=Иванов&FirstName=Иван&Patronymic=Иванович
        public Employee GetByName(string LastName, string FirstName, string Patronymic) =>
           _EmployeesData.GetByName(LastName, FirstName, Patronymic);

        [HttpPost]
        public int Add(Employee employee)
        {
            _Logger.LogInformation("Добавление сотрудника {0}", employee.ToString());

            return _EmployeesData.Add(employee);
        }

        [HttpPost("employee")] // post -> https://localhost:5001/api/employees/employee?LastName=Иванов&FirstName=Иван&Patronymic=Иванович&Age=37
        public Employee Add(string LastName, string FirstName, string Patronymic, int Age)
        {
            _Logger.LogInformation("Добавление сотрудника {0} {1}, {2}", LastName, FirstName, Patronymic);

            return _EmployeesData.Add(LastName, FirstName, Patronymic, Age);
        }

        [HttpPut] // put -> http://localhost:5001/api/employees/5
        public void Update(Employee employee)
        {
            _Logger.LogInformation("Редактирование сотрудника {0}", employee.ToString());

            _EmployeesData.Update(employee);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            _Logger.LogInformation("Удаление сотрудника id:{0}", id);

            var result =  _EmployeesData.Delete(id);

            if(result) _Logger.LogInformation("Удаление сотрудника id:{0} успешно выполнено...", id);
            else _Logger.LogInformation("Удаление сотрудника id:{0} не выполнено", id);

            return result;
        }
    }
}
