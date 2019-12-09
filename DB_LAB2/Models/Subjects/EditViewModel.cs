using DB_LAB2.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Subjects
{
    public class EditViewModel
    {
        public EditViewModel(Subject subject)
        {
            Id = subject.Id;
            Title = subject.Title;
        }

        [Required(ErrorMessage = "Не вказаний id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Не вказана назва предмету!")]
        public string Title { get; set; }
    }
}
