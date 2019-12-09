using DB_LAB2.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Students
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
        }

        public CreateViewModel(Student student)
        {
            Id = student.Id;
            LastName = student.LastName;
            FirstName = student.FirstName;
            Birthday = student.Birthday;
            GroupId = student.GroupId;
        }

        [Required(ErrorMessage = "Не вказаний id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Не вказане ім'я студента!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не вказане прізвище студента!!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не вказане день народження студента!")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "Не вказана група студента!")]
        public long GroupId { get; set; }

    }
}
