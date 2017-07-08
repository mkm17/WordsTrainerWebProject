using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using WordsTrainerWeb.db;

[assembly: OwinStartup(typeof(WordsTrainerWeb.Startup))]

namespace WordsTrainerWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           
            ConfigureAuth(app);
        }
    }
}
