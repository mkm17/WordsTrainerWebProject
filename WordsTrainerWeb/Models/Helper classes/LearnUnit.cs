using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordsTrainerWeb.Models.Helper_classes
{
    public class LearnUnit
    {
        public UserWord userWord { get; set; }
        public IEnumerable<string> listOfOptions { get; set; }
        public string exerciseType { get; set; }

        public LearnUnit(UserWord word, IEnumerable<string> listOfResponses, string type)
        {
            userWord = word;
            listOfOptions = listOfResponses;
            exerciseType = type;
        }

    }
}