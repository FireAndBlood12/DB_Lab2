using DB_LAB2.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB_LAB2.Models.Home
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
        }

        public IndexViewModel(string includedWordSearch, string fullPhraseSearch, string searchType, List<SearchRes> results)
        {
            IncludedWordSearch = includedWordSearch;
            FullPhraseSearch = fullPhraseSearch;
            SearchType = searchType;
            Results = results;
        }

        public string IncludedWordSearch { get; set; }
        public string FullPhraseSearch { get; set; }
        public string SearchType { get; set; }
        public List<SearchRes> Results { get; set; }
    }
}
