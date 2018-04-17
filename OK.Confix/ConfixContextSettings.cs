using System.Collections.Generic;

namespace OK.Confix
{
    internal class ConfixContextSettings
    {
        public string Application { get; set; }

        public string Environment { get; set; }

        public IDataProvider DataProvider { get; set; }

        public int? CacheInterval { get; set; }

        public List<ActionSubscription> ActionSubscriptions { get; set; }

        public ConfixContextSettings()
        {
            this.ActionSubscriptions = new List<ActionSubscription>();
        }
    }
}