using DB_LAB2.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Subjects
{
    public class DeleteViewMode
    {
        public DeleteViewMode(Subject subject)
        {
            Subject = subject;
        }

        public Subject Subject { get; set; }

    }
}
