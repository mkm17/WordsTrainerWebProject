using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WordsTrainerWeb.Models
{
    public class UserSettings
    {
        [Key]
        public int id { get; set; }
        public int numberOfWordsPerLearning { get; set; }
        public virtual Language currentLangage { get; set; }

    }
}