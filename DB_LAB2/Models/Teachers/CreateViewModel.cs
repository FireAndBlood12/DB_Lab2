using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Teachers
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
        }
  
        [Required(ErrorMessage = "Не вказаний id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Не вказане ім'я викладача!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не вказане прізвище викладача!!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не вказаний досвід викладача!")]
        public int Experience { get; set; }

        [Required(ErrorMessage = "Не вказаний основний предмет викладача!")]
        public long MainSubjectId { get; set; }

    }
}
