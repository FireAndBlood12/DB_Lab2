﻿using DB_LAB2.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Marks
{
    public class DetailsViewModel
    {
        public DetailsViewModel(Mark mark)
        {
            Mark = mark;
        }

        public Mark Mark { get; set; }
    }
}
