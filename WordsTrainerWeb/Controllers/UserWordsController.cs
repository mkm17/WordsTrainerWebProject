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
using WordsTrainerWeb.Models.Helper_classes;
using System.Collections;

namespace WordsTrainerWeb.Controllers
{
    [RequireHttps]
    [Authorize]
    public class UserWordsController : Controller
    {
        WordsContext wordcontext = new WordsContext();

        // GET: UserWords
        public ActionResult Index()
        {
            UserCl usercl = wordcontext.Users.Where(x => x.name == User.Identity.Name).FirstOrDefault();
            if (usercl.languages.Count < 1) { return RedirectToAction("Index", "ChooseLanguage"); }
            IComparer<UserWord> comparer = new UserWordsLvlComparer<UserWord>();
                return View(wordcontext.UserWords.Include(x => x.word).Where(x =>
                x.user.id == usercl.id &&
                x.word.foreignLanguage.id == usercl.settings.currentLangage.id)
               .ToList().Where(x => x.levelOfKnowledge != 0).OrderByDescending(x => x.levelOfKnowledge));
           
        }
        [HttpPost]
        public JsonResult LevelUp(int option)
        {
            using (wordcontext = new WordsContext())
            {
                UserCl usercl = wordcontext.Users.Where(x => x.name == User.Identity.Name).FirstOrDefault();
                wordcontext.UserWords.Where(x => x.id == option).First().levelOfKnowledge = 10;
                wordcontext.SaveChanges();
                return Json(JsonRequestBehavior.AllowGet);
            }
        }
    }
}
