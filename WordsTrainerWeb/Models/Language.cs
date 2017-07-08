using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WordsTrainerWeb.Models
{
    public class Language
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public virtual ICollection<Word> words { get; set; }
        public virtual ICollection<UserCl> users { get; set; }
        public override string ToString()
        {
            return "Language";
        }
    }
}