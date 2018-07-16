using OK.Confix.Configuration.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OK.Confix.Configuration
{
    public class Configurator : IConfigurator
    {
        private Confix _confix;

        public Configurator(Confix confix)
        {
            _confix = confix;
        }

        public IWithConfigurator UseConfix(Action<IConfixConfigurator> config)
        {
            IConfixConfigurator confixConfigurator = new ConfixConfigurator(_confix);

            config.Invoke(confixConfigurator);

            return new WithConfigurator(this);
        }

        public IConfixContext Build()
        {
            List<Action> buildActions = _confix.Context.Settings.ActionSubscriptions
                                                                .Where(x => x.ActionType == ActionType.Build)
                                                                .Select(x => x.Action)
                                                                .ToList();

            foreach (var buildAction in buildActions)
            {
                buildAction.Invoke();
            }

            _confix.Context.Build();

            return _confix.Context;
        }

        public IDataManager GetDataManager()
        {
            return _confix.Context.Settings.DataManager;
        }

        public void SetDataProvider(IDataProvider dataProvider)
        {
            _confix.Context.Settings.DataProvider = dataProvider;
        }

        public void SubscribeAction(ActionType actionType, Action action)
        {
            string cancelationToken;

            SubscribeAction(actionType, action, out cancelationToken);
        }

        public void SubscribeAction(ActionType actionType, Action action, out string cancelationToken)
        {
            cancelationToken = Guid.NewGuid().ToString();

            ActionSubscription actionSubscription = new ActionSubscription()
            {
                Token = cancelationToken,
                ActionType = actionType,
                Action = action
            };

            _confix.Context.Settings.ActionSubscriptions.Add(actionSubscription);
        }

        public void UnsubscribeAction(string cancelationToken)
        {
            ActionSubscription actionSubscription = _confix.Context.Settings.ActionSubscriptions
                                                                            .FirstOrDefault(x => x.Token == cancelationToken);

            if (actionSubscription != null)
            {
                _confix.Context.Settings.ActionSubscriptions.Remove(actionSubscription);
            }
        }
    }
}