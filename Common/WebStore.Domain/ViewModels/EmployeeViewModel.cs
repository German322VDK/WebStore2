using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; init; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 15 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Неверный формат имени")]
        public string FirstName { get; init; }

        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия обязательна")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 15 символов")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Неверный формат фамилии")]
        public string LastName { get; init; }

        [Display(Name = "Отчество")]
        public string Patronymic { get; init; }

        [Display(Name = "Должность")]
        public string Job { get; init; }

        [Display(Name = "Возраст")]
        [Range(18, 80, ErrorMessage = "Сотрудник должен быть в возрасте от 18 до 80 лет")]
        public int Age { get; init; }

        [Display(Name = "Зарплата")]
        public int Salary { get; init; }

        [Display(Name = "Трудовой стаж")]
        public int WorkExperience { get; init; }
    }
}
