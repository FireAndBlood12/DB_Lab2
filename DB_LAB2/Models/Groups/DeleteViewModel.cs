using DB_LAB2.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Groups
{
    public class DeleteViewMode
    {
        public DeleteViewMode(Group group)
        {
            Group = group;
        }

        public Group Group { get; set; }

    }
}
