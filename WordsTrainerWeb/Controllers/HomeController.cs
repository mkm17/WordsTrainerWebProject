using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;
using WordsTrainerWeb.db;
using WordsTrainerWeb.Models;
using WordsTrainerWeb.Models.Helper_classes;

namespace WordsTrainerWeb.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        UserCl usercl;
        WordsContext wordContext;

        [CustomAnonymousToken]
        public ActionResult Index()
        {

            using (wordContext = new WordsContext())
            {
                bool isAuthenticated = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
                if (isAuthenticated)
                {
                    usercl = wordContext.Users.Where(x => x.name == User.Identity.Name).FirstOrDefault();
                }

                return View();
                
            }
        }

        [Authorize]
        public ActionResult Learn()
        {
            using (wordContext = new WordsContext())
            {
                try {
                    usercl = wordContext.Users.Where(x => x.name == User.Identity.Name).FirstOrDefault();

                    if (usercl.languages.Count < 1) { return RedirectToAction("Index", "ChooseLanguage"); }

                    IEnumerable<Language> userLanguages = usercl.languages.ToList();
                    SelectList sl = new SelectList(from e in userLanguages
                                                   select new
                                                   {
                                                       Value = e.id,
                                                       Text = e.name
                                                   }, "Value", "Text");
                    return View(sl);
                }
                catch(Exception e)
                {
                    return View("Error");
                }
            }
        }
        [HttpPost]
        public JsonResult GetWords(String option)
        {

            object traning;
            wordContext = new WordsContext();
            LearningStrategy ls;
            usercl = wordContext.Users.Where(x => x.name == User.Identity.Name).FirstOrDefault();
            int learningStrategy = usercl.settings.numberOfWordsPerLearning;
            ls = new NormalLearningStrategy();

            switch (learningStrategy)
            {
                case 1:
                    ls = new NormalLearningStrategy();
                    break;
                case 2:
                    ls = new NewWordsStrategy();
                    break;
                default:
                    break;
            }
            string exerciseType;
            List<LearnUnit> learnUnits = new List<LearnUnit>();
            List<string> listOfOption;
            Random randomNumber = new Random();

            int languageName = usercl.settings.currentLangage.id;
            //take all words where level of knowledge (1-10) is smaller than 6 
            IEnumerable<UserWord> wordsForLearn = new List<UserWord>();
            int numberOfLanguageWords = wordContext.UserWords.Where(x => x.word.foreignLanguage.id == languageName).Count();

            if (wordsForLearn.Count() < ls.getSumOFWords())
            {
                wordsForLearn = wordsForLearn.Union<UserWord>(wordContext.UserWords
                    .Where(x => x.levelOfKnowledge < 3 && x.levelOfKnowledge > 0
                    && x.word.foreignLanguage.id == languageName
                    && x.user.id == usercl.id).Take(ls.getBasicWords()));

            }
            if (wordsForLearn.Count() < ls.getSumOFWords())
            {
                wordsForLearn = wordsForLearn.Union<UserWord>(wordContext.UserWords
                    .Where(x => x.levelOfKnowledge >= 3 && x.levelOfKnowledge < 7
                    && x.word.foreignLanguage.id == languageName
                    && x.user.id == usercl.id).Take(ls.getIntermediateWords()));

            }
            if (wordsForLearn.Count() < ls.getSumOFWords())
            {
                wordsForLearn = wordsForLearn.Union<UserWord>(wordContext.UserWords
                    .Where(x => x.levelOfKnowledge >= 7 && x.levelOfKnowledge < 9
                    && x.word.foreignLanguage.id == languageName
                    && x.user.id == usercl.id).Take(ls.getUpperIntermediateWords()));

            }
            if (wordsForLearn.Count() < ls.getSumOFWords())
            {
                wordsForLearn = wordsForLearn.Union<UserWord>(wordContext.UserWords
                    .Where(x => x.levelOfKnowledge >= 9
                    && x.word.foreignLanguage.id == languageName
                    && x.user.id == usercl.id).OrderBy(x=> x.lastTimeAccesed).Take(ls.getMasterWords()));

            }

            if (wordsForLearn.Count() < ls.getSumOFWords())
            {
                wordsForLearn = wordsForLearn.Union<UserWord>(wordContext.UserWords
                    .Where(x => x.levelOfKnowledge == 0 && x.word.foreignLanguage.id == languageName
                    && x.user.id == usercl.id).Take(ls.getSumOFWords() - wordsForLearn.Count()));

            }

            foreach (UserWord singleWord in wordsForLearn)
            {
                listOfOption = new List<string>();


                switch (singleWord.levelOfKnowledge)
                {
                    case 0:
                        listOfOption.Add(singleWord.word.foreignText);
                        exerciseType = LearningType.Learning.name;
                        break;
                    case 1:
                        exerciseType = LearningType.Choose.name;
                        listOfOption.Add(singleWord.word.foreignText);
                        listOfOption.Add(wordContext.UserWords.Where(x => x.word.foreignLanguage.id == languageName).ToList().ElementAt(randomNumber.Next(0, numberOfLanguageWords - 1)).word.foreignText);
                        break;

                    case 2:
                    case 3:
                    case 4:
                        exerciseType = LearningType.Choose.name;
                        listOfOption.Add(singleWord.word.foreignText);
                        listOfOption.Add(wordContext.UserWords.Where(x => x.word.foreignLanguage.id == languageName).ToList().ElementAt(randomNumber.Next(0, numberOfLanguageWords - 1)).word.foreignText);
                        listOfOption.Add(wordContext.UserWords.Where(x => x.word.foreignLanguage.id == languageName).ToList().ElementAt(randomNumber.Next(0, numberOfLanguageWords - 1)).word.foreignText);
                        listOfOption.Add(wordContext.UserWords.Where(x => x.word.foreignLanguage.id == languageName).ToList().ElementAt(randomNumber.Next(0, numberOfLanguageWords - 1)).word.foreignText);
                        break;

                    case 5:
                    case 6:
                        exerciseType = LearningType.Choose.name;
                        listOfOption.Add(singleWord.word.foreignText);
                        listOfOption.Add(wordContext.UserWords.Where(x => x.word.foreignLanguage.id == languageName).ToList().ElementAt(randomNumber.Next(0, numberOfLanguageWords - 1)).word.foreignText);
                        listOfOption.Add(wordContext.UserWords.Where(x => x.word.foreignLanguage.id == languageName).ToList().ElementAt(randomNumber.Next(0, numberOfLanguageWords - 1)).word.foreignText);
                        listOfOption.Add(wordContext.UserWords.Where(x => x.word.foreignLanguage.id == languageName).ToList().ElementAt(randomNumber.Next(0, numberOfLanguageWords - 1)).word.foreignText);
                        listOfOption.Add(wordContext.UserWords.Where(x => x.word.foreignLanguage.id == languageName).ToList().ElementAt(randomNumber.Next(0, numberOfLanguageWords - 1)).word.foreignText);
                        listOfOption.Add(wordContext.UserWords.Where(x => x.word.foreignLanguage.id == languageName).ToList().ElementAt(randomNumber.Next(0, numberOfLanguageWords - 1)).word.foreignText);
                        break;

                    case 7:
                    case 8:
                        exerciseType = LearningType.Writing.name;
                        //http://creativyst.com/Doc/Articles/SoundEx1/SoundEx1.htm
                        break;
                    case 9:
                    case 10:
                        exerciseType = LearningType.Writing.name;
                        break;
                    default:
                        throw new NotImplementedException();


                }
                listOfOption.Shuffle();
                learnUnits.Add(new LearnUnit(singleWord, listOfOption, exerciseType));

            }

            traning = learnUnits
              .Select(a => new
              {
                  WordID = a.userWord.id,
                  Word = a.userWord.word.homeText,
                  WordExt = a.userWord.word.foreignText,
                  Level = a.userWord.levelOfKnowledge,
                  Exercise = a.exerciseType,
                  Options = a.listOfOptions

              });

            return Json(traning, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetResults(Dictionary<string, object> option)
        {
            bool isAuthenticated = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!isAuthenticated) { return null; }

            UserWord checkedWord;
            List<ResultType> listOfResults = new List<ResultType>();
            Dictionary<string, object> resultList = option;
            double result;
            bool okWord;
            double okWords = 0;
            double subResult;
            object Jresult;

            wordContext = new WordsContext();



            usercl = wordContext.Users.Where(x => x.name == User.Identity.Name).FirstOrDefault();

            foreach (var cell in resultList)
            {

                checkedWord = usercl.usersWords.Where(x => x.id == Convert.ToInt32(cell.Key)).First();
                if ( checkedWord.word.foreignText.ToLower() == Convert.ToString(cell.Value).ToLower())
                {
                    if (checkedWord.levelOfKnowledge < 10)
                    {
                        checkedWord.levelOfKnowledge += 1;
                    }
                    else
                    {
                        checkedWord.levelOfKnowledge = 10;
                    }
                    okWord = true;
                    okWords++;
                }
                else
                {
                    okWord = false;
                    checkedWord.levelOfKnowledge -= 1;
                }
                checkedWord.lastTimeAccesed = DateTime.Now;
                listOfResults.Add(new ResultType(checkedWord.word.homeText, checkedWord.word.foreignText,
                    Convert.ToString(cell.Value), checkedWord.levelOfKnowledge, okWord));
            }
            subResult = okWords / resultList.Count;
            result = Convert.ToInt32(Math.Round(subResult, 2) * 100);


            var listOfWords = listOfResults
                  .Select(a => new
                  {
                      Word = a.word,
                      WordExt = a.extWord,
                      Level = a.currentLevel,
                      LevelUp = a.levlUp,
                      YourResult = a.yourResponse
                  });

            Jresult = new { Result = result, List = listOfWords };
            wordContext.SaveChanges();

            return Json(Jresult, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetUserRemainingWords()
        {
            bool isAuthenticated = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!isAuthenticated) { return null; }

            using (wordContext = new WordsContext())
            {
                usercl = wordContext.Users.Where(x => x.name == User.Identity.Name).FirstOrDefault();

                int numberOfWords = wordContext.UserWords.Where(x => x.user.name == User.Identity.Name
                && x.word.foreignLanguage.id == usercl.settings.currentLangage.id).Count();


                numberOfWords -= wordContext.Words.Where(x => x.foreignLanguage.id
                == usercl.settings.currentLangage.id).Count();


                return Json(numberOfWords * -1, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult AddNewLanguageWords()
        {
            bool isAuthenticated = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!isAuthenticated) { return null; }

            using (wordContext = new WordsContext())
            {
                usercl = wordContext.Users.Where(x => x.name == User.Identity.Name).FirstOrDefault();
                IEnumerable<UserWord> userLanguageWords = usercl.usersWords.Where(x => x.word.foreignLanguage.id == usercl.settings.currentLangage.id);
                IQueryable<Word> newLanguageWords = wordContext.Words.Where(x => x.foreignLanguage.id == usercl.settings.currentLangage.id);
                int numberOfNewWords = 0;
                foreach (Word word in newLanguageWords)
                {
                    if (userLanguageWords.FirstOrDefault(x => x.word.id == word.id) == null)
                    {
                        usercl.usersWords.Add(new UserWord() { lastTimeAccesed = DateTime.Now, levelOfKnowledge = 0, user = usercl, word = word });
                        numberOfNewWords++;
                    }
                }

                wordContext.SaveChanges();

                return Json(numberOfNewWords, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult GetCurrentLanguage()
        {
            bool isAuthenticated = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (!isAuthenticated) { return null; }
            using (wordContext = new WordsContext())
            {
                usercl = wordContext.Users.Where(x => x.name == User.Identity.Name).FirstOrDefault();

                return Json(usercl.settings.currentLangage.name, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
