using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WordsTrainerWeb.Models;

namespace WordsTrainerWeb.db
{
    public class WordsInicializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<WordsContext>
    {
        //DropCreateDatabaseAlways  DropCreateDatabaseIfModelChanges
        protected override void Seed(WordsContext context)
        {
            var w = context.Database;

            context.SaveChanges();
        }
    }
}


