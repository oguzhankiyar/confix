using OK.Confix.Configuration.Core;
using System;
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

        public ConfixContext Build()
        {
            _confix.Context.Build();

            return _confix.Context;
        }

        public IDataProvider GetDataProvider()
        {
            return _confix.Context.Settings.DataProvider;
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

            _confix.Context.Settings.ActionSubscriptions.Add(new ActionSubscription()
            {
                Token = cancelationToken,
                ActionType = actionType,
                Action = action
            });
        }

        public void UnsubscribeAction(string cancelationToken)
        {
            ActionSubscription actionSubscription = _confix.Context.Settings.ActionSubscriptions.FirstOrDefault(x => x.Token == cancelationToken);

            if (actionSubscription != null)
                _confix.Context.Settings.ActionSubscriptions.Remove(actionSubscription);
        }
    }
}