using OK.Confix.Models;
using System.Collections.Generic;

namespace OK.Confix
{
    public interface IDataManager
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);


        List<ApplicationModel> GetApplications();
        ApplicationModel GetApplication(int id);
        bool CreateApplication(string name);
        bool EditApplication(int id, string name);
        bool DeleteApplication(int id);


        List<EnvironmentModel> GetEnvironments();
        EnvironmentModel GetEnvironment(int id);
        bool CreateEnvironment(int applicationId, string name);
        bool EditEnvironment(int id, int applicationId, string name);
        bool DeleteEnvironment(int id);


        List<ConfigurationModel> GetConfigurations();
        ConfigurationModel GetConfiguration(int id);
        bool CreateConfiguration(int applicationId, int? environmentId, string name, object value);
        bool EditConfiguration(int id, int applicationId, int? environmentId, string name, object value);
        bool DeleteConfiguration(int id);
    }
}