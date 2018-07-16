using OK.Confix.Managers;
using OK.Confix.Models;
using System.Collections.Generic;

namespace OK.Confix
{
    internal class ConfixContextSettings
    {
        public ApplicationModel Application { get; set; }

        public EnvironmentModel Environment { get; set; }

        public string ApplicationName { get; set; }

        public string EnvironmentName { get; set; }

        public IDataManager DataManager { get; set; }

        public IDataProvider DataProvider { get; set; }

        public int? CacheInterval { get; set; }

        public List<ActionSubscription> ActionSubscriptions { get; set; }

        public ConfixContextSettings()
        {
            DataManager = new DefaultDataManager(this);
            ActionSubscriptions = new List<ActionSubscription>();
        }
    }
}