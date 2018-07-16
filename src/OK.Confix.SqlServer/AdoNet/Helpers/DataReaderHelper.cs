using OK.Confix.SqlServer.Core.Entities;
using System;
using System.Data.SqlClient;

namespace OK.Confix.SqlServer.AdoNet.Helpers
{
    internal static class DataReaderHelper
    {
        public static ApplicationEntity MapApplication(SqlDataReader dataReader)
        {
            ApplicationEntity application = new ApplicationEntity();

            application.Id = Convert.ToInt32(dataReader["ApplicationId"]);
            application.Name = Convert.ToString(dataReader["ApplicationName"]);
            application.IsDeleted = Convert.ToBoolean(dataReader["ApplicationIsDeleted"]);
            application.CreatedDate = Convert.ToDateTime(dataReader["ApplicationCreatedDate"]);
            application.UpdatedDate = Convert.ToDateTime(dataReader["ApplicationUpdatedDate"]);

            return application;
        }

        public static EnvironmentEntity MapEnvironment(SqlDataReader dataReader)
        {
            EnvironmentEntity environment = new EnvironmentEntity();

            environment.Id = Convert.ToInt32(dataReader["EnvironmentId"]);
            environment.ApplicationId = Convert.ToInt32(dataReader["EnvironmentApplicationId"]);
            environment.Name = Convert.ToString(dataReader["EnvironmentName"]);
            environment.IsDeleted = Convert.ToBoolean(dataReader["EnvironmentIsDeleted"]);
            environment.CreatedDate = Convert.ToDateTime(dataReader["EnvironmentCreatedDate"]);
            environment.UpdatedDate = Convert.ToDateTime(dataReader["EnvironmentUpdatedDate"]);

            environment.Application = MapApplication(dataReader);

            return environment;
        }

        public static ConfigurationEntity MapConfiguration(SqlDataReader dataReader)
        {
            ConfigurationEntity configuration = new ConfigurationEntity();

            configuration.Id = Convert.ToInt32(dataReader["ConfigurationId"]);
            configuration.ApplicationId = Convert.ToInt32(dataReader["ConfigurationApplicationId"]);
            configuration.EnvironmentId = Convert.ToInt32(dataReader["ConfigurationEnvironmentId"]);
            configuration.Name = Convert.ToString(dataReader["ConfigurationName"]);
            configuration.Type = Convert.ToString(dataReader["ConfigurationType"]);
            configuration.Value = Convert.ToString(dataReader["ConfigurationValue"]);
            configuration.IsDeleted = Convert.ToBoolean(dataReader["ConfigurationIsDeleted"]);
            configuration.CreatedDate = Convert.ToDateTime(dataReader["ConfigurationCreatedDate"]);
            configuration.UpdatedDate = Convert.ToDateTime(dataReader["ConfigurationUpdatedDate"]);

            configuration.Application = MapApplication(dataReader);
            configuration.Environment = MapEnvironment(dataReader);

            return configuration;
        }
    }
}