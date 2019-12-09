using DB_LAB2.Database.Entities;
using DB_LAB2.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Subjects
{
    public class SubjectsViewModel
    {
        public IEnumerable<Subject> Subjects { get; set; }
        public PageViewModel PageViewModel { get; set; }

        public SubjectsViewModel()
        {
        }

        public SubjectsViewModel(IEnumerable<Subject> subjects, PageViewModel pageViewModel)
        {
            Subjects = subjects;
            PageViewModel = pageViewModel;
        }
    }
}
