using OK.Confix.Helpers;
using OK.Confix.WebUI.Api.Inputs;
using OK.Confix.WebUI.Api.Results;

namespace OK.Confix.WebUI.Api.Controllers
{
    internal class ApplicationsController : BaseController
    {
        private readonly IDataManager _dataManager;

        public ApplicationsController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public string Process(string action, string body)
        {
            object result = null;

            switch (action)
            {
                case "list":
                    result = ListApplications();
                    break;
                case "details":
                    DetailApplicationInput detailApplicationInput = JsonHelper.Deserialize<DetailApplicationInput>(body);
                    result = DetailApplication(detailApplicationInput);
                    break;
                case "create":
                    CreateApplicationInput createApplicationInput = JsonHelper.Deserialize<CreateApplicationInput>(body);
                    result = CreateApplication(createApplicationInput);
                    break;
                case "edit":
                    EditApplicationInput editApplicationInput = JsonHelper.Deserialize<EditApplicationInput>(body);
                    result = EditApplication(editApplicationInput);
                    break;
                case "delete":
                    DeleteApplicationInput deleteApplicationInput = JsonHelper.Deserialize<DeleteApplicationInput>(body);
                    result = DeleteApplication(deleteApplicationInput);
                    break;
            }

            return JsonHelper.Serialize(result);
        }

        public ListApplicationResult ListApplications()
        {
            ListApplicationResult result = new ListApplicationResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.GetApplications();
                },
                (value) =>
                {
                    result.IsSuccessful = true;
                    result.ApplicationList = value;
                },
                (message) =>
                {
                    result.Message = message;
                });

            return result;
        }

        public DetailApplicationResult DetailApplication(DetailApplicationInput model)
        {
            DetailApplicationResult result = new DetailApplicationResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.GetApplication(model.Id);
                },
                (value) =>
                {
                    result.IsSuccessful = true;
                    result.Application = value;
                },
                (message) =>
                {
                    result.IsSuccessful = false;
                    result.Message = message;
                });

            return result;
        }

        public CreateApplicationResult CreateApplication(CreateApplicationInput model)
        {
            CreateApplicationResult result = new CreateApplicationResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.CreateApplication(model.Name);
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

        public EditApplicationResult EditApplication(EditApplicationInput model)
        {
            EditApplicationResult result = new EditApplicationResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.EditApplication(model.Id, model.Name);
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

        public DeleteApplicationResult DeleteApplication(DeleteApplicationInput model)
        {
            DeleteApplicationResult result = new DeleteApplicationResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.DeleteApplication(model.Id);
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
    }
}