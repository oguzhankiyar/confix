using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OK.Confix.Models;
using OK.Confix.WebUI.Api.Inputs;
using OK.Confix.WebUI.Api.Results;

namespace OK.Confix.WebUI.Api.Controllers
{

    public class ApplicationsController
    {
        private IDataProvider _dataProvider;

        public ApplicationsController(IDataProvider dataProvider)
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
                    result = ListApplications();
                    break;
                case "details":
                    DetailApplicationInput detailApplicationInput = JsonConvert.DeserializeObject<DetailApplicationInput>(body, serializerSettings);
                    result = DetailApplication(detailApplicationInput);
                    break;
                case "create":
                    CreateApplicationInput createApplicationInput = JsonConvert.DeserializeObject<CreateApplicationInput>(body, serializerSettings);
                    result = CreateApplication(createApplicationInput);
                    break;
                case "edit":
                    EditApplicationInput editApplicationInput = JsonConvert.DeserializeObject<EditApplicationInput>(body, serializerSettings);
                    result = EditApplication(editApplicationInput);
                    break;
                case "delete":
                    DeleteApplicationInput deleteApplicationInput = JsonConvert.DeserializeObject<DeleteApplicationInput>(body, serializerSettings);
                    result = DeleteApplication(deleteApplicationInput);
                    break;
            }

            return JsonConvert.SerializeObject(result, serializerSettings);
        }

        public ListApplicationResult ListApplications()
        {
            return new ListApplicationResult()
            {
                IsSuccessful = true,
                ApplicationList = _dataProvider.GetApplications()
            };
        }

        public DetailApplicationResult DetailApplication(DetailApplicationInput model)
        {
            return new DetailApplicationResult()
            {
                IsSuccessful = true,
                Application = _dataProvider.GetApplication(model.Id)
            };
        }

        public CreateApplicationResult CreateApplication(CreateApplicationInput model)
        {
            bool isSuccess = _dataProvider.InsertApplication(new ApplicationModel() { Name = model.Name });

            return new CreateApplicationResult() { IsSuccessful = isSuccess };
        }

        public EditApplicationResult EditApplication(EditApplicationInput model)
        {
            bool isSuccess = _dataProvider.UpdateApplication(new ApplicationModel() { Id = model.Id, Name = model.Name });

            return new EditApplicationResult()
            {
                IsSuccessful = isSuccess
            };
        }

        public DeleteApplicationResult DeleteApplication(DeleteApplicationInput model)
        {
            bool isSuccess = _dataProvider.RemoveApplication(model.Id);

            return new DeleteApplicationResult()
            {
                IsSuccessful = isSuccess
            };
        }
    }
}