using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WordsTrainerWeb.Models
{
    public class Word
    {
        [Key]
        public int id { get; set; }
        public virtual Category category { get; set; }
        public virtual Language homeLanguage { get; set; }
        public virtual Language foreignLanguage { get; set; }
        public string homeText { get; set; }
        public string foreignText { get; set; }
        public string description { get; set; }
        public virtual ICollection<Word> similarWords { get; set; }
        public virtual PartOfSpeach partOfSpeach { get; set; }
        public virtual ICollection<UserWord> userWords { get; set; }
        //public int levelOfKnowledge { get; set; }
        //public DateTime lastTimeAccesed { get; set; }
    }
}