using OK.Confix.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OK.Confix
{
    public class ConfixContext
    {
        internal ConfixContextSettings Settings { get; set; }

        internal IDataManager DataManager;

        public ConfixContext()
        {
            Settings = new ConfixContextSettings();
            DataManager = new DefaultDataManager(this);
        }

        internal void Build()
        {
            List<Action> buildActions = Settings.ActionSubscriptions
                                                .Where(x => x.ActionType == ActionType.Build)
                                                .Select(x => x.Action)
                                                .ToList();

            foreach (var buildAction in buildActions)
            {
                buildAction.Invoke();
            }
        }

        public T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return default(T);
            }

            return DataManager.Get<T>(key);
        }

        public void Set<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                return;
            }

            DataManager.Set(key, value);
        }
    }
}