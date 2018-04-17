using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OK.Confix.Models;
using OK.Confix.WebUI.Api.Inputs;
using OK.Confix.WebUI.Api.Results;

namespace OK.Confix.WebUI.Api.Controllers
{
    class ConfigurationsController
    {
        private IDataProvider _dataProvider;

        public ConfigurationsController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public string Process(string action, string body)
        {
            var serializerSettings = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            object result = null;

            switch (action)
            {
                case "list":
                    result = ListConfigurations();
                    break;
                case "details":
                    DetailConfigurationInput detailConfigurationInput = JsonConvert.DeserializeObject<DetailConfigurationInput>(body, serializerSettings);
                    result = DetailConfiguration(detailConfigurationInput);
                    break;
                case "create":
                    CreateConfigurationInput createConfigurationInput = JsonConvert.DeserializeObject<CreateConfigurationInput>(body, serializerSettings);
                    result = CreateConfiguration(createConfigurationInput);
                    break;
                case "edit":
                    EditConfigurationInput editConfigurationInput = JsonConvert.DeserializeObject<EditConfigurationInput>(body, serializerSettings);
                    result = EditConfiguration(editConfigurationInput);
                    break;
                case "delete":
                    DeleteConfigurationInput deleteConfigurationInput = JsonConvert.DeserializeObject<DeleteConfigurationInput>(body, serializerSettings);
                    result = DeleteConfiguration(deleteConfigurationInput);
                    break;
            }

            return JsonConvert.SerializeObject(result, serializerSettings);
        }

        public ListConfigurationResult ListConfigurations()
        {
            return new ListConfigurationResult()
            {
                IsSuccessful = true,
                ConfigurationList = _dataProvider.GetConfigurations()
            };
        }

        public DetailConfigurationResult DetailConfiguration(DetailConfigurationInput model)
        {
            return new DetailConfigurationResult()
            {
                IsSuccessful = true,
                Configuration = _dataProvider.GetConfiguration(model.Id)
            };
        }

        public CreateConfigurationResult CreateConfiguration(CreateConfigurationInput model)
        {
            bool isSuccess = _dataProvider.InsertConfiguration(new ConfigurationModel() { ApplicationId = model.ApplicationId, EnvironmentId = model.EnvironmentId, Name = model.Name, Value = model.Value });

            return new CreateConfigurationResult()
            {
                IsSuccessful = isSuccess
            };
        }

        public EditConfigurationResult EditConfiguration(EditConfigurationInput model)
        {
            bool isSuccess = _dataProvider.UpdateConfiguration(new ConfigurationModel() { Id = model.Id, ApplicationId = model.ApplicationId, EnvironmentId = model.EnvironmentId, Name = model.Name, Value = model.Value });

            return new EditConfigurationResult()
            {
                IsSuccessful = isSuccess
            };
        }

        public DeleteConfigurationResult DeleteConfiguration(DeleteConfigurationInput model)
        {
            bool isSuccess = _dataProvider.RemoveConfiguration(model.Id);

            return new DeleteConfigurationResult()
            {
                IsSuccessful = isSuccess
            };
        }
    }
}