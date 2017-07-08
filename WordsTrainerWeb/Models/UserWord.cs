using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WordsTrainerWeb.Models
{
    public class UserWord
    {
        [Key]
        public int id { get; set; }
        public int levelOfKnowledge { get; set; }
        public DateTime lastTimeAccesed { get; set; }
        public virtual Word word {get;set;}
        public virtual UserCl user { get; set; }

    }
    
}