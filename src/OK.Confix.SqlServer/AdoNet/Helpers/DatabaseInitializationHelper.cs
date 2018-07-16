using OK.Confix.SqlServer.AdoNet.DataContexts;

namespace OK.Confix.SqlServer.AdoNet.Helpers
{
	internal static class DatabaseInitializationHelper
	{
		public static bool InitializeDatabase(ConfixDataContext dataContext)
		{
			bool isSuccess = false;

			isSuccess = CreateConfixSchema(dataContext);

			if (!isSuccess)
				return isSuccess;

			isSuccess = CreateApplicationsTable(dataContext);

			if (!isSuccess)
				return isSuccess;

			isSuccess = CreateEnvironmentsTable(dataContext);

			if (!isSuccess)
				return isSuccess;

			isSuccess = CreateConfigurationsTable(dataContext);

			if (!isSuccess)
				return isSuccess;

			return isSuccess;
		}

		#region Helpers

		private static bool CreateConfixSchema(ConfixDataContext dataContext)
		{
			const string sql = @"IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'cfx')
								 BEGIN
									 EXEC('CREATE SCHEMA [cfx]')
								 END
								 
								 SELECT 1;";

			return dataContext.ExecuteScalar<int>(sql) == 1;
		}

		private static bool CreateApplicationsTable(ConfixDataContext dataContext)
		{
			const string sql = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cfx].[Applications]') AND type in (N'U'))
								BEGIN
									CREATE TABLE [cfx].[Applications] (
										 [Id] [INT] PRIMARY KEY IDENTITY(1,1) NOT NULL,
										 [Name] [NVARCHAR](MAX) NOT NULL,
										 [IsDeleted] [BIT] NOT NULL,
										 [CreatedDate] [DATETIME] NOT NULL,
										 [UpdatedDate] [DATETIME] NOT NULL
									 );
								 END

								 SELECT 1;";

			return dataContext.ExecuteScalar<int>(sql) == 1;
		}

		private static bool CreateEnvironmentsTable(ConfixDataContext dataContext)
		{
			const string sql = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cfx].[Environments]') AND type in (N'U'))
								BEGIN
									CREATE TABLE [cfx].[Environments](
										 [Id] [INT] PRIMARY KEY IDENTITY(1,1) NOT NULL,
										 [ApplicationId] [INT] NOT NULL,
										 [Name] [NVARCHAR](MAX) NOT NULL,
										 [IsDeleted] [BIT] NOT NULL,
										 [CreatedDate] [DATETIME] NOT NULL,
										 [UpdatedDate] [DATETIME] NOT NULL
									 );

									 ALTER TABLE [cfx].[Environments]
									 ADD CONSTRAINT [FK_cfx.Environments_cfx.Applications_ApplicationId]
									 FOREIGN KEY([ApplicationId])
									 REFERENCES [cfx].[Applications] ([Id]);
								 END

								 SELECT 1;";

			return dataContext.ExecuteScalar<int>(sql) == 1;
		}

		private static bool CreateConfigurationsTable(ConfixDataContext dataContext)
		{
			const string sql = @"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[cfx].[Configurations]') AND type in (N'U'))
								BEGIN
									CREATE TABLE [cfx].[Configurations](
										 [Id] [INT] PRIMARY KEY IDENTITY(1,1) NOT NULL,
										 [ApplicationId] [INT] NOT NULL,
										 [EnvironmentId] [INT] NULL,
										 [Name] [NVARCHAR](MAX) NOT NULL,
										 [Type] [NVARCHAR](10) NOT NULL,
										 [Value] [NVARCHAR](MAX) NOT NULL,
										 [IsDeleted] [BIT] NOT NULL,
										 [CreatedDate] [DATETIME] NOT NULL,
										 [UpdatedDate] [DATETIME] NOT NULL
									 );

									 ALTER TABLE [cfx].[Configurations]
									 ADD CONSTRAINT [FK_cfx.Configurations_cfx.Applications_ApplicationId]
									 FOREIGN KEY([ApplicationId])
									 REFERENCES [cfx].[Applications] ([Id]);
								 
									 ALTER TABLE [cfx].[Configurations]
									 ADD CONSTRAINT [FK_cfx.Configurations_cfx.Environments_EnvironmentId]
									 FOREIGN KEY([EnvironmentId])
									 REFERENCES [cfx].[Environments] ([Id]);
								 END

								 SELECT 1;";

			return dataContext.ExecuteScalar<int>(sql) == 1;
		}

		#endregion
	}
}