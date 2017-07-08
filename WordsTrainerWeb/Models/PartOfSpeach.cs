using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WordsTrainerWeb.Models
{
    public class PartOfSpeach
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public virtual ICollection<Word> words { get; set; }
    }
}