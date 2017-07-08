using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordsTrainerWeb.Models.Helper_classes
{
    public class LearningStrategy
    {
        public int id { get; set; }
        public string name { get; set; }
        public virtual int getNewWords() { return 0; }
        public virtual int getBasicWords() { return 0; }
        public virtual int getIntermediateWords() { return 0; }
        public virtual int getUpperIntermediateWords() { return 0; }
        public virtual int getMasterWords() { return 0; }
        public int getSumOFWords() {
            return getBasicWords() + getIntermediateWords() + getUpperIntermediateWords() + getMasterWords() + getNewWords();
        }
    }
}