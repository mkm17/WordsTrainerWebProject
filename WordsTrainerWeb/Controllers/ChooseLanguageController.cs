using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WordsTrainerWeb.db;
using WordsTrainerWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WordsTrainerWeb.Models.Helper_classes;

namespace WordsTrainerWeb.Controllers
{
    [RequireHttps]
    [Authorize]
    public class ChooseLanguageController : Controller
    {
        UserCl usercl;
        WordsContext wordcontext;
        // GET: ChooseLanguage

        public ActionResult Index()
        {

            using (wordcontext = new WordsContext())
            {
                usercl = wordcontext.Users.Where(x => x.name == User.Identity.Name).FirstOrDefault();
                List<Language> languagesToChoose;
                List<LearningStrategy> strategiesToChoose=new List<LearningStrategy>();
                NormalLearningStrategy s1 = new NormalLearningStrategy();
                NewWordsStrategy s2 = new NewWordsStrategy();
                MediumLearningStrategy s3 = new MediumLearningStrategy();
                strategiesToChoose.Add(s1);
                strategiesToChoose.Add(s2);
                strategiesToChoose.Add(s3);

                languagesToChoose = wordcontext.Languages.ToList();

                ViewBag.Strategies = strategiesToChoose;
                ViewBag.Languages = languagesToChoose;
                
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "language,strategy")]LanguageAndStrategyContener lanStr)
        {
            using (wordcontext = new WordsContext())
            {
                UserCl usercl = wordcontext.Users.Where(x => x.name == User.Identity.Name).FirstOrDefault();
                if (usercl.languages.Where(x => x.id == lanStr.language.id).FirstOrDefault() == null)
                {


                    if (usercl.languages.ToList() == null)
                    {
                        usercl.languages = new List<Language>();
                    }
                    if (usercl.usersWords.ToList() == null)
                    {
                        usercl.usersWords = new List<UserWord>();
                    }
                    IQueryable<Word> languageWords = wordcontext.Words.Where(x => x.foreignLanguage.id == lanStr.language.id);
                    foreach (Word word in languageWords)
                    {
                        usercl.usersWords.Add(new UserWord() { lastTimeAccesed = DateTime.Now, levelOfKnowledge = 0, user = usercl, word = word });
                    }
                    usercl.languages.Add(wordcontext.Languages.Where(x => x.id == lanStr.language.id).First());
                }
                usercl.settings.currentLangage = usercl.languages.FirstOrDefault(x => x.id == lanStr.language.id);
                usercl.settings.numberOfWordsPerLearning = lanStr.strategy.id;
                wordcontext.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
        }
    }
}