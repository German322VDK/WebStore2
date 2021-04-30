using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Domain.Models;

namespace WebStore.Controllers
{
    //[Route("staff")]

    /*[Authorize(Roles = Role.Administrator + "," + Role.Users)] - У меня получилось разрешить 
    не сколько ролей только таким способом */

    //[Authorize(Roles = Role.Administrator)]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _IEmployeesData;

        public EmployeesController(IEmployeesData IEmployeesData) => _IEmployeesData = IEmployeesData;

        //[Route("all")]
        public IActionResult Index() => View(_IEmployeesData.Get());

        //[Route("info(id-{id})")]
        public IActionResult Details(int id) // http://localhost:5000/employees/details/2
        {
            var employee = _IEmployeesData.Get(id);
            if (employee is not null)
                return View(employee);
            return NotFound();
        }

        [Authorize(Roles = Role.Administrator)]
        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        #region Edit
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(int id)
        {
            if (id <= 0) return BadRequest();

            var employee = _IEmployeesData.Get(id);

            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Job = employee.Job,
                Age = employee.Age,
                Salary = employee.Salary,
                WorkExperience = employee.WorkExperience
            });
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Patronymic = model.Patronymic,
                    Job = model.Job,
                    Age = model.Age,
                    Salary = model.Salary,
                    WorkExperience = model.WorkExperience
                };

                if (employee.Id == 0)
                    _IEmployeesData.Add(employee);
                else
                    _IEmployeesData.Update(employee);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }

        #endregion

        #region Delete
        [Authorize(Roles = Role.Administrator)]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var employee = _IEmployeesData.Get((int)id);

            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Job = employee.Job,
                Age = employee.Age,
                Salary = employee.Salary,
                WorkExperience = employee.WorkExperience
            });
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrator)]
        public IActionResult DeleteConfirmed(int id)
        {
            _IEmployeesData.Delete(id);

            return RedirectToAction("Index");
        }

        #endregion

    }
}
