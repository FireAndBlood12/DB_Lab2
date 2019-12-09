using DB_LAB2.Database.Entities;
using DB_LAB2.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Teachers
{
    public class TeachersViewModel
    {
        public IEnumerable<Teacher> Teachers { get; set; }
        public PageViewModel PageViewModel { get; set; }

        public int MinExp { get; set; }
        public int MaxExp { get; set; }
        public string SubjectTitle { get; set; }
        public TeachersViewModel()
        {
        }

        public TeachersViewModel(IEnumerable<Teacher> teachers, PageViewModel pageViewModel)
        {
            Teachers = teachers;
            PageViewModel = pageViewModel;
        }
    }
}
