using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordsTrainerWeb.Models.Helper_classes
{
    public class NormalLearningStrategy : LearningStrategy
    {
        public NormalLearningStrategy()
        {
            this.id = 1;
            this.name = "Normal Learning Strategy";
        }
        public override int getBasicWords()
        {
            return 4;
        }

        public override int getIntermediateWords()
        {
            return 7;
        }

        public override int getMasterWords()
        {
            return 3;
        }

        public override int getNewWords()
        {
            return 3;
        }
        public override int getUpperIntermediateWords()
        {
            return 4;
        }
    }
}