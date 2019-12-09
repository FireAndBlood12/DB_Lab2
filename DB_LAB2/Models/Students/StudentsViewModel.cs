using DB_LAB2.Database.Entities;
using DB_LAB2.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Students
{
    public class StudentsViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public PageViewModel PageViewModel { get; set; }

        public StudentsViewModel()
        {
        }

        public StudentsViewModel(IEnumerable<Student> students, PageViewModel pageViewModel)
        {
            Students = students;
            PageViewModel = pageViewModel;
        }
    }
}
