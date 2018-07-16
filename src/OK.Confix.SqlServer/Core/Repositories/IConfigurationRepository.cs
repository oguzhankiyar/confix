using OK.Confix.SqlServer.Core.Entities;
using System.Collections.Generic;

namespace OK.Confix.SqlServer.Core.Repositories
{
    internal interface IConfigurationRepository
    {
        IEnumerable<ConfigurationEntity> FindConfigurations();

        IEnumerable<ConfigurationEntity> FindConfigurations(int applicationId, string name);

        IEnumerable<ConfigurationEntity> FindConfigurationsByApplication(int applicationId);

        IEnumerable<ConfigurationEntity> FindConfigurationsByEnvironment(int? environmentId);

        ConfigurationEntity FindConfiguration(int id);

        ConfigurationEntity FindConfiguration(int applicationId, int? environmentId, string name);

        ConfigurationEntity InsertConfiguration(ConfigurationEntity configuration);

        bool UpdateConfiguration(ConfigurationEntity configuration);

        bool RemoveConfiguration(ConfigurationEntity configuration);
    }
}