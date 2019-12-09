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
using DB_LAB2.Models.Marks;

namespace DB_LAB2.Controllers
{
    public class MarksController : Controller
    {
        private DAO<Subject> subjectDAO;
        private DAO<Teacher> teacherDAO;
        private DAO<Student> studentDAO;
        private DAO<Mark> markDAO;

        public MarksController(DB_LAB2Context context)
        {
            teacherDAO = new TeacherDAO(DBConnection.getInstance());
            subjectDAO = new SubjectDAO(DBConnection.getInstance());
            studentDAO = new StudentDAO(DBConnection.getInstance());
            markDAO = new MarkDAO(DBConnection.getInstance());
        }

        // GET: Marks
        public IActionResult Index(int page = 1)
        {
            PageViewModel pageViewModel = new PageViewModel(teacherDAO.Count(), page, 10);
            MarksViewModel model = new MarksViewModel(markDAO.GetList(page), pageViewModel);
            return View(model);
        }

        // GET: Marks/Details/5
        public IActionResult Details(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var mark = markDAO.GetById(id);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // GET: Marks/Create
        public IActionResult Create()
        {
            ViewData["StudentId"] = new SelectList(studentDAO.GetList(0), "Id", "LastName");
            ViewData["SubjectId"] = new SelectList(subjectDAO.GetList(0), "Id", "Title");
            ViewData["TeacherId"] = new SelectList(teacherDAO.GetList(0), "Id", "LastName");
            return View(new CreateViewModel());
        }

        // POST: Marks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Mark_,Date,SubjectId,StudentId,TeacherId")] Mark mark)
        {
            if (ModelState.IsValid)
            {
                markDAO.Create(mark);
                return RedirectToAction(nameof(Index));
            }
            ViewData["StudentId"] = new SelectList(studentDAO.GetList(0), "Id", "LastName");
            ViewData["SubjectId"] = new SelectList(subjectDAO.GetList(0), "Id", "Title");
            ViewData["TeacherId"] = new SelectList(teacherDAO.GetList(0), "Id", "LastName");
            return View(new CreateViewModel(mark));
        }

        // GET: Marks/Edit/5
        public IActionResult Edit(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var mark = markDAO.GetById(id);
            if (mark == null)
            {
                return NotFound();
            }
            ViewData["StudentId"] = new SelectList(studentDAO.GetList(0), "Id", "LastName");
            ViewData["SubjectId"] = new SelectList(subjectDAO.GetList(0), "Id", "Title");
            ViewData["TeacherId"] = new SelectList(teacherDAO.GetList(0), "Id", "LastName");
            return View(new EditViewModel(mark));
        }

        // POST: Marks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,Mark_,Date,SubjectId,StudentId,TeacherId")] Mark mark)
        {
            if (id != mark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    markDAO.Update(mark);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarkExists(mark.Id))
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
            ViewData["StudentId"] = new SelectList(studentDAO.GetList(0), "Id", "LastName");
            ViewData["SubjectId"] = new SelectList(subjectDAO.GetList(0), "Id", "Title");
            ViewData["TeacherId"] = new SelectList(teacherDAO.GetList(0), "Id", "LastName");
            return View(new EditViewModel(mark));
        }

        // GET: Marks/Delete/5
        public IActionResult Delete(long id)
        {
            if (id < 1)
            {
                return NotFound();
            }

            var mark = markDAO.GetById(id);

            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // POST: Marks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            markDAO.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool MarkExists(long id)
        {
            return markDAO.GetById(id) != null;
        }
    }
}
