using DB_LAB2.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Groups
{
    public class EditViewModel
    {
        public EditViewModel(Group group)
        {
            Id = group.Id;
            Code = group.Code;
        }

        [Required(ErrorMessage = "Не вказаний id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Не вказана назва групи!")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Не вказана дата вступу!")]
        public DateTime EntranceYear { get; set; }
    }
}
