using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Database.Entities
{
    public class Group
    {
        private long id;

        private String code;

        private DateTime entranceYear;

        public Group()
        {
        }

        public Group(long id, string code, DateTime entranceYear)
        {
            this.id = id;
            this.code = code;
            this.entranceYear = entranceYear;
        }

        public string Code { get => code; set => code = value; }
        public long Id { get => id; set => id = value; }
        public DateTime EntranceYear { get => entranceYear; set => entranceYear = value; }
    }
}
