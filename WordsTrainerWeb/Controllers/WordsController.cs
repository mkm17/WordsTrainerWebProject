using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WordsTrainerWeb.Models;
using WordsTrainerWeb.db;

namespace WordsTrainerWeb.Controllers
{
    [RequireHttps]
    [Authorize]
    public class WordsController : Controller
    {
        // GET: Words
        WordsContext wordcontext = new WordsContext();
        public ActionResult Index()
        {
            return View(wordcontext.Words.ToList());
        }

        // GET: Words/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = wordcontext.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // GET: Words/Create
        public ActionResult Create()
        {
            ViewBag.Languages = wordcontext.Languages;
            ViewBag.Words = wordcontext.Words;
            ViewBag.PartOfSpeech = wordcontext.Parts;
            ViewBag.Categories = wordcontext.Categories;
            return View();
        }

        // POST: Words/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,category,partOfSpeach,homeText,homeLanguage,foreignLanguage,foreignText,description")]Word word)
        {
            if (ModelState.IsValid)
            {
                Language homeLanguage= wordcontext.Languages.FirstOrDefault(x=>x.id==word.homeLanguage.id);
                Language foreignLanguage= wordcontext.Languages.FirstOrDefault(x => x.id == word.homeLanguage.id);
                PartOfSpeach partOfSpeach= wordcontext.Parts.FirstOrDefault(x => x.id == word.partOfSpeach.id);
                Category category= wordcontext.Categories.FirstOrDefault(x => x.id == word.category.id);
                word.homeLanguage = homeLanguage;
                word.foreignLanguage = foreignLanguage;
                word.partOfSpeach = partOfSpeach;
                word.category = category;
                wordcontext.Words.Add(word);
                wordcontext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(word);
        }

        // GET: Words/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = wordcontext.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // POST: Words/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,homeText,foreignText,description")] Word word)
        {
            if (ModelState.IsValid)
            {
                wordcontext.Entry(word).State = EntityState.Modified;
                wordcontext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(word);
        }

        // GET: Words/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Word word = wordcontext.Words.Find(id);
            if (word == null)
            {
                return HttpNotFound();
            }
            return View(word);
        }

        // POST: Words/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Word word = wordcontext.Words.Find(id);
            wordcontext.Words.Remove(word);
            wordcontext.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                wordcontext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
