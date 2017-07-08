using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordsTrainerWeb.Models.Helper_classes
{
    public class MediumLearningStrategy : LearningStrategy
    {
        public MediumLearningStrategy()
        {
            this.id = 3;
            this.name = "Middle Words Strategy";
        }
        public override int getBasicWords()
        {
            return 1;
        }

        public override int getIntermediateWords()
        {
            return 9;
        }

        public override int getMasterWords()
        {
            return 3;
        }

        public override int getNewWords()
        {
            return 1;
        }
        public override int getUpperIntermediateWords()
        {
            return 6;
        }
    }
}