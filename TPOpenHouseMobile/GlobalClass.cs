using System;
using System.Collections.Generic;
using System.Text;

namespace TPOpenHouseMobile
{
    public class GlobalClass
    {
        public class User
        {

            public string userID { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
            public int points { get; set; }

        }
        public class Event
        {
            public int ID { get; set; }
            public string eventName { get; set; }
            public string eventVenue { get; set; }
            public DateTime eventTime { get; set; }
        }

        public class Question
        {
            public int ID { get; set; }
            public string AnswerOne { get; set; }
            public string AnswerTwo { get; set; }
            public string AnswerThree { get; set; }
            public string AnswerFour { get; set; }
            public string Correct { get; set; }
            public string questionCategory { get; set; }
            public string questionString { get; set; }
        }

        public class UserResponse
        {
            public int ID { get; set; }
            public string userIDFK { get; set; }
            public int questionIDFK { get; set; }
            public string userAnswer { get; set; }
            public bool isCorrect { get; set; }

        }
    }
}
