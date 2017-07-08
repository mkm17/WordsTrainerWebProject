using System;
using System.Collections;
using System.Collections.Generic;
using WordsTrainerWeb.Models;

namespace WordsTrainerWeb.Models.Helper_classes
{
    public class UserWordsLvlComparer<T> : IComparer<T>
    {
        public int Compare(T t1, T t2)
        {
            UserWord c1 = (UserWord)(object)t1;
            UserWord c2 = (UserWord)(object)t2;
            if (c1.levelOfKnowledge > c2.levelOfKnowledge)
                return 1;

            if (c1.levelOfKnowledge < c2.levelOfKnowledge)
                return -1;

            else
                return 0;
        }

    }
}