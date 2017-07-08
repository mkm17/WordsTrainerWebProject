using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordsTrainerWeb.Models.Helper_classes
{
    public class ResultType
    {
        public string word { get; set; }
        public string extWord { get; set; }
        public string yourResponse { get; set; }
        public int currentLevel { get; set; }
        public bool levlUp { get; set; }

        public ResultType(string _word, string _extWord, string _yourResp, int _lvl, bool _lvlUp)
        {
            word = _word;
            extWord = _extWord;
            yourResponse = _yourResp;
            currentLevel = _lvl;
            levlUp = _lvlUp;
        }
    }
}