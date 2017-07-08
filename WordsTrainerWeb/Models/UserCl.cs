using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WordsTrainerWeb.db;
using Microsoft.AspNet.Identity;

namespace WordsTrainerWeb.Models
{
    public class UserCl
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public virtual ICollection<UserWord> usersWords { get; set; }
        public virtual ICollection<Language> languages { get; set; }
        public virtual UserSettings settings { get; set; }

        private static UserCl userCl;

        public static UserCl getCurrentUser(string name)
        {
            if(userCl==null || userCl.name!=name)
            {
                userCl= CreateCurrentUser(name);
            }
            return userCl;
        }

        private static UserCl CreateCurrentUser(string userName)
        {
            return WordsContext.Get().Users.Where(x => x.name == userName).FirstOrDefault();
        }
    }
}