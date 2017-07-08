using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordsTrainerWeb.Models.Helper_classes
{
    public class NewWordsStrategy : LearningStrategy
    {

        public NewWordsStrategy()
        {
            this.id = 2;
            this.name = "NewWordsStrategy";
        }
        public override int getBasicWords()
        {
            return 5;
        }

        public override int getIntermediateWords()
        {
            return 3;
        }

        public override int getMasterWords()
        {
            return 0;
        }

        public override int getNewWords()
        {
            return 12;
        }
        public override int getUpperIntermediateWords()
        {
            return 1;
        }
    }
}