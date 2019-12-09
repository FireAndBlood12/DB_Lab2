using DB_LAB2.Database.Entities;
using DB_LAB2.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Marks
{
    public class MarksViewModel
    {
        public IEnumerable<Mark> Marks { get; set; }
        public PageViewModel PageViewModel { get; set; }

        public MarksViewModel()
        {
        }

        public MarksViewModel(IEnumerable<Mark> marks, PageViewModel pageViewModel)
        {
            Marks = marks;
            PageViewModel = pageViewModel;
        }
    }
}
