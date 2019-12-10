using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Home
{
    public class RandomPackageViewModel
    {
        public RandomPackageViewModel()
        {
        }

        public RandomPackageViewModel(int count, string message)
        {
            Count = count;
            Message = message;
        }

        public int Count { get; set; }
        public string Message { get; set; }
    }
}
