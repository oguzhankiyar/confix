using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OK.Confix.Models;
using OK.Confix.WebUI.Api.Inputs;
using OK.Confix.WebUI.Api.Results;

namespace OK.Confix.WebUI.Api.Controllers
{
    class EnvironmentsController
    {
        private IDataProvider _dataProvider;

        public EnvironmentsController(IDataProvider dataProvider)
        {
            this._dataProvider = dataProvider;
        }

        public string Process(string action, string body)
        {
            var serializerSettings = new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() };

            object result = null;

            switch (action)
            {
                case "list":
                    result = ListEnvironments();
                    break;
                case "details":
                    DetailEnvironmentInput detailEnvironmentInput = JsonConvert.DeserializeObject<DetailEnvironmentInput>(body, serializerSettings);
                    result = DetailEnvironment(detailEnvironmentInput);
                    break;
                case "create":
                    CreateEnvironmentInput createEnvironmentInput = JsonConvert.DeserializeObject<CreateEnvironmentInput>(body, serializerSettings);
                    result = CreateEnvironment(createEnvironmentInput);
                    break;
                case "edit":
                    EditEnvironmentInput editEnvironmentInput = JsonConvert.DeserializeObject<EditEnvironmentInput>(body, serializerSettings);
                    result = EditEnvironment(editEnvironmentInput);
                    break;
                case "delete":
                    DeleteEnvironmentInput deleteEnvironmentInput = JsonConvert.DeserializeObject<DeleteEnvironmentInput>(body, serializerSettings);
                    result = DeleteEnvironment(deleteEnvironmentInput);
                    break;
            }

            return JsonConvert.SerializeObject(result, serializerSettings);
        }

        public ListEnvironmentResult ListEnvironments()
        {
            return new ListEnvironmentResult()
            {
                IsSuccessful = true,
                EnvironmentList = _dataProvider.GetEnvironments()
            };
        }

        public DetailEnvironmentResult DetailEnvironment(DetailEnvironmentInput model)
        {
            return new DetailEnvironmentResult()
            {
                IsSuccessful = true,
                Environment = _dataProvider.GetEnvironment(model.Id)
            };
        }

        public CreateEnvironmentResult CreateEnvironment(CreateEnvironmentInput model)
        {
            bool isSuccess = _dataProvider.InsertEnvironment(new EnvironmentModel() { ApplicationId = model.ApplicationId, Name = model.Name });

            return new CreateEnvironmentResult()
            {
                IsSuccessful = isSuccess
            };
        }

        public EditEnvironmentResult EditEnvironment(EditEnvironmentInput model)
        {
            bool isSuccess = _dataProvider.UpdateEnvironment(new EnvironmentModel() { Id = model.Id, ApplicationId = model.ApplicationId, Name = model.Name });

            return new EditEnvironmentResult()
            {
                IsSuccessful = isSuccess
            };
        }

        public DeleteEnvironmentResult DeleteEnvironment(DeleteEnvironmentInput model)
        {
            bool isSuccess = _dataProvider.RemoveEnvironment(model.Id);

            return new DeleteEnvironmentResult()
            {
                IsSuccessful = isSuccess
            };
        }
    }
}