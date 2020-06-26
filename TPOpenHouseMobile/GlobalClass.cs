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

    }
}
