using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database.Entities
{
    public class Subject
    {
        private long id;

        private String title;

        public Subject()
        {
        }

        public Subject(long id, string title)
        {
            this.id = id;
            this.title = title;
        }

        public long Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }


    }
}
