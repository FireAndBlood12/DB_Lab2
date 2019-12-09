using DB_LAB2.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Marks
{
    public class EditViewModel
    {
        public EditViewModel(Mark mark)
        {
            Id = mark.Id;
            Mark_ = mark.Mark_;
            Date = mark.Date;
            SubjectId = mark.SubjectId;
            TeacherId = mark.TeacherId;
            StudentId = mark.StudentId;
        }

        [Required(ErrorMessage = "Не вказаний id")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Не вказаний бал оцінки!")]
        public int Mark_ { get; set; }

        [Required(ErrorMessage = "Не вказана дата виставлення оцінки!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Не вказаний предмет!")]
        public long SubjectId { get; set; }

        [Required(ErrorMessage = "Не вказаний студент!")]
        public long StudentId { get; set; }

        [Required(ErrorMessage = "Не вказаний викладач!")]
        public long TeacherId { get; set; }
    }
}
