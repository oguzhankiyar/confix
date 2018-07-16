using System;

namespace OK.Confix
{
    public class ActionSubscription
    {
        public string Token { get; set; }

        public ActionType ActionType { get; set; }

        public Action Action { get; set; }
    }
}