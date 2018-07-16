using OK.Confix.Helpers;
using OK.Confix.Models;
using OK.Confix.WebUI.Api.Inputs;
using OK.Confix.WebUI.Api.Results;
using System;

namespace OK.Confix.WebUI.Api.Controllers
{
    internal class ConfigurationsController : BaseController
    {
        private readonly IDataManager _dataManager;

        public ConfigurationsController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public string Process(string action, string body)
        {
            object result = null;

            switch (action)
            {
                case "list":
                    result = ListConfigurations();
                    break;
                case "details":
                    DetailConfigurationInput detailConfigurationInput = JsonHelper.Deserialize<DetailConfigurationInput>(body);
                    result = DetailConfiguration(detailConfigurationInput);
                    break;
                case "create":
                    CreateConfigurationInput createConfigurationInput = JsonHelper.Deserialize<CreateConfigurationInput>(body);
                    result = CreateConfiguration(createConfigurationInput);
                    break;
                case "edit":
                    EditConfigurationInput editConfigurationInput = JsonHelper.Deserialize<EditConfigurationInput>(body);
                    result = EditConfiguration(editConfigurationInput);
                    break;
                case "delete":
                    DeleteConfigurationInput deleteConfigurationInput = JsonHelper.Deserialize<DeleteConfigurationInput>(body);
                    result = DeleteConfiguration(deleteConfigurationInput);
                    break;
            }

            return JsonHelper.Serialize(result);
        }

        public ListConfigurationResult ListConfigurations()
        {
            ListConfigurationResult result = new ListConfigurationResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.GetConfigurations();
                },
                (value) =>
                {
                    result.IsSuccessful = true;
                    result.ConfigurationList = value;
                },
                (message) =>
                {
                    result.Message = message;
                });

            return result;
        }

        public DetailConfigurationResult DetailConfiguration(DetailConfigurationInput model)
        {
            DetailConfigurationResult result = new DetailConfigurationResult();

            TryGetResult(
                () =>
                {
                    ConfigurationModel conf = _dataManager.GetConfiguration(model.Id);

                    conf.Value = conf.Value.Trim('"');

                    return conf;
                },
                (value) =>
                {
                    result.IsSuccessful = true;
                    result.Configuration = value;
                },
                (message) =>
                {
                    result.IsSuccessful = false;
                    result.Message = message;
                });

            return result;
        }

        public CreateConfigurationResult CreateConfiguration(CreateConfigurationInput model)
        {
            CreateConfigurationResult result = new CreateConfigurationResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.CreateConfiguration(model.ApplicationId, model.EnvironmentId, model.Name, ChangeObjectType(model.Value, model.Type));
                },
                (value) =>
                {
                    result.IsSuccessful = value;
                },
                (message) =>
                {
                    result.IsSuccessful = false;
                    result.Message = message;
                });

            return result;
        }

        public EditConfigurationResult EditConfiguration(EditConfigurationInput model)
        {
            EditConfigurationResult result = new EditConfigurationResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.EditConfiguration(model.Id, model.ApplicationId, model.EnvironmentId, model.Name, ChangeObjectType(model.Value, model.Type));
                },
                (value) =>
                {
                    result.IsSuccessful = value;
                },
                (message) =>
                {
                    result.IsSuccessful = false;
                    result.Message = message;
                });

            return result;
        }

        public DeleteConfigurationResult DeleteConfiguration(DeleteConfigurationInput model)
        {
            DeleteConfigurationResult result = new DeleteConfigurationResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.DeleteConfiguration(model.Id);
                },
                (value) =>
                {
                    result.IsSuccessful = value;
                },
                (message) =>
                {
                    result.IsSuccessful = false;
                    result.Message = message;
                });

            return result;
        }

        #region Helpers

        private object ChangeObjectType(string value, string type)
        {
            switch (type)
            {
                case "String":
                    return Convert.ToString(value);
                case "Integer":
                    return Convert.ToInt32(value);
                case "Decimal":
                    return Convert.ToDecimal(value);
                case "Boolean":
                    return Convert.ToBoolean(value);
                case "DateTime":
                    return Convert.ToDateTime(value);
                case "Object":
                    return JsonHelper.Deserialize<object>(value);
                default:
                    return Convert.ToString(value);
            }
        }

        #endregion
    }
}