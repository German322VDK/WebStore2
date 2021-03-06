using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Job { get; set; }

        public int Age { get; set; }

        public int Salary { get; set; }

        public int WorkExperience { get; set; }

        public override string ToString() => $"{Id} {FirstName} {LastName} {Patronymic}";

    }
}
