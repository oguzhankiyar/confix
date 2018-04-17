using System;

namespace OK.Confix
{
    internal class ActionSubscription
    {
        public string Token { get; set; }

        public ActionType ActionType { get; set; }

        public Action Action { get; set; }
    }
}