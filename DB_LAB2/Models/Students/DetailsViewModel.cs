using DB_LAB2.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Students
{
    public class DetailsViewModel
    {
        public DetailsViewModel(Student student)
        {
            Student = student;
        }

        public Student Student { get; set; }
    }
}
