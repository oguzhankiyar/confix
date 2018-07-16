using OK.Confix.Models;
using System.Collections.Generic;

namespace OK.Confix
{
    public interface IDataProvider
    {
        List<ApplicationModel> GetApplications();
        ApplicationModel GetApplication(int id);
        ApplicationModel GetApplication(string name);
        bool InsertApplication(ApplicationModel application);
        bool UpdateApplication(ApplicationModel application);
        bool RemoveApplication(int id);

        List<EnvironmentModel> GetEnvironments();
        List<EnvironmentModel> GetEnvironmentsByApplication(int applicationId);
        EnvironmentModel GetEnvironment(int id);
        EnvironmentModel GetEnvironment(string name);
        bool InsertEnvironment(EnvironmentModel environment);
        bool UpdateEnvironment(EnvironmentModel environment);
        bool RemoveEnvironment(int id);

        List<ConfigurationModel> GetConfigurations();
        List<ConfigurationModel> GetConfigurations(int applicationId, string key);
        List<ConfigurationModel> GetConfigurationsByApplication(int applicationId);
        List<ConfigurationModel> GetConfigurationsByEnvironment(int? environmentId);
        ConfigurationModel GetConfiguration(int id);
        ConfigurationModel GetConfiguration(int applicationId, int? environmentId, string key);
        bool InsertConfiguration(ConfigurationModel configuration);
        bool UpdateConfiguration(ConfigurationModel configuration);
        bool RemoveConfiguration(int id);
    }
}