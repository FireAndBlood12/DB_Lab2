using DB_LAB2.Database.Entities;
using DB_LAB2.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Groups
{
    public class GroupsViewModel
    {
        public IEnumerable<Group> Groups { get; set; }
        public PageViewModel PageViewModel { get; set; }

        public GroupsViewModel()
        {
        }

        public GroupsViewModel(IEnumerable<Group> groups, PageViewModel pageViewModel)
        {
            Groups = groups;
            PageViewModel = pageViewModel;
        }
    }
}
