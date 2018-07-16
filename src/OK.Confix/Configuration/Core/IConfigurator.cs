using System;

namespace OK.Confix.Configuration.Core
{
    public interface IConfigurator
    {
        IWithConfigurator UseConfix(Action<IConfixConfigurator> config);

        IConfixContext Build();

        IDataManager GetDataManager();

        void SetDataProvider(IDataProvider dataProvider);

        void SubscribeAction(ActionType actionType, Action action);

        void SubscribeAction(ActionType actionType, Action action, out string cancelationToken);

        void UnsubscribeAction(string cancelationToken);
    }
}