using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Subjects
{
    public class CreateViewModel
    {
        public CreateViewModel()
        {
        }

        public CreateViewModel(long id, string title)
        {
            Id = id;
            Title = title;
        }

        [Required(ErrorMessage = "Не вказаний id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Не вказана назва предмету!")]
        public string Title { get; set; }

    }
}
