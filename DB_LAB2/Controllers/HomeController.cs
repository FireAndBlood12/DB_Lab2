using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DB_LAB2.Models;
using Npgsql;
using DB_LAB2.Models.Home;
using DB_LAB2.Database;

namespace DB_LAB2.Controllers
{
    public class HomeController : Controller
    {
        private FullTextSearch textSearch;

        public HomeController()
        {
            textSearch = new FullTextSearch(DBConnection.getInstance());
        }
        public IActionResult Index(IndexViewModel viewModel)
        {
            if(viewModel == null) return View(new IndexViewModel());

            if (viewModel.FullPhraseSearch != null && viewModel.FullPhraseSearch.Length > 0)
            {
                return View(new IndexViewModel("", viewModel.FullPhraseSearch, "Full Text Search",
                                                textSearch.getFullPhrase("title", "subjects", viewModel.FullPhraseSearch)));
            }
            if (viewModel.IncludedWordSearch != null && viewModel.IncludedWordSearch.Length > 0)
            {
                return View(new IndexViewModel( viewModel.IncludedWordSearch, "", "Included Word Search",
                                                textSearch.getAllWithIncludedWord("title", "subjects", viewModel.IncludedWordSearch)));
            }
            return View(new IndexViewModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
