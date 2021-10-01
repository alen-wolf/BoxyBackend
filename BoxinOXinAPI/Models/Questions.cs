using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoxinOXinAPI.Models
{
    public class Questions
    {
        public int id { get; set; }

        public string Question { get; set; }

        public string CorrectAnswer { get; set; }

        public string WrongAnswerOne { get; set; }

        public string WrongAnswerTwo { get; set; }

    }
}
