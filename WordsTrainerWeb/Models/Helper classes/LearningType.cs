using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordsTrainerWeb.Models.Helper_classes
{
    public sealed class LearningType
    {

        public  String name { get; set; }
        private readonly int value;

        public static readonly LearningType Learning = new LearningType(1, "Learning");
        public static readonly LearningType Choose = new LearningType(2, "Choose");
        public static readonly LearningType Writing = new LearningType(3, "Writing");

        private LearningType(int value, String name)
        {
            this.name = name;
            this.value = value;
        }

        public override String ToString()
        {
            return name;
        }

    }
}