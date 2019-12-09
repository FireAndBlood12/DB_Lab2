using DB_LAB2.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Teachers
{
    public class DeleteViewMode
    {
        public DeleteViewMode(Teacher teacher)
        {
            Teacher = teacher;
        }

        public Teacher Teacher { get; set; }

    }
}
