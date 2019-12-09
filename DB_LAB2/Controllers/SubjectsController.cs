using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DB_LAB2.Database.Entities;
using DB_LAB2.Models;
using DB_LAB2.Database;
using DB_LAB2.Models.Shared;
using DB_LAB2.Models.Subjects;

namespace DB_LAB2.Controllers
{
    public class SubjectsController : Controller
    {
        private DAO<Subject> subjectDAO;

        public SubjectsController()
        {
            subjectDAO = new SubjectDAO(DBConnection.getInstance());
        }

        // GET: Subjects
        public IActionResult Index(int page = 1)
        {
            PageViewModel pageViewModel = new PageViewModel(subjectDAO.Count(), page, 10);
            SubjectsViewModel model = new SubjectsViewModel(subjectDAO.GetList(page), pageViewModel);
            return View(model);
        }

        // GET: Subjects/Details/5
        public IActionResult Details(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var subject = subjectDAO.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                subjectDAO.Create(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(new CreateViewModel(subject.Id, subject.Title));
        }

        // GET: Subjects/Edit/5
        public IActionResult Edit(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var subject = subjectDAO.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(new EditViewModel(subject));
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,Title")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    subjectDAO.Update(subject);
                }
                catch (Exception)
                {
                    if (!SubjectExists(subject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(new EditViewModel(subject));
        }

        // GET: Subjects/Delete/5
        public IActionResult Delete(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var subject = subjectDAO.GetById(id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            if (!SubjectExists(id))
            {
                return NotFound();
            }
            subjectDAO.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(long id)
        {
            return subjectDAO.GetById(id) != null;
        }
    }
}
