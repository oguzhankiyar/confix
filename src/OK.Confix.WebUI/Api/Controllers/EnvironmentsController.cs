using OK.Confix.Helpers;
using OK.Confix.WebUI.Api.Inputs;
using OK.Confix.WebUI.Api.Results;

namespace OK.Confix.WebUI.Api.Controllers
{
    internal class EnvironmentsController : BaseController
    {
        private readonly IDataManager _dataManager;

        public EnvironmentsController(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public string Process(string action, string body)
        {
            object result = null;

            switch (action)
            {
                case "list":
                    result = ListEnvironments();
                    break;
                case "details":
                    DetailEnvironmentInput detailEnvironmentInput = JsonHelper.Deserialize<DetailEnvironmentInput>(body);
                    result = DetailEnvironment(detailEnvironmentInput);
                    break;
                case "create":
                    CreateEnvironmentInput createEnvironmentInput = JsonHelper.Deserialize<CreateEnvironmentInput>(body);
                    result = CreateEnvironment(createEnvironmentInput);
                    break;
                case "edit":
                    EditEnvironmentInput editEnvironmentInput = JsonHelper.Deserialize<EditEnvironmentInput>(body);
                    result = EditEnvironment(editEnvironmentInput);
                    break;
                case "delete":
                    DeleteEnvironmentInput deleteEnvironmentInput = JsonHelper.Deserialize<DeleteEnvironmentInput>(body);
                    result = DeleteEnvironment(deleteEnvironmentInput);
                    break;
            }

            return JsonHelper.Serialize(result);
        }

        public ListEnvironmentResult ListEnvironments()
        {
            ListEnvironmentResult result = new ListEnvironmentResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.GetEnvironments();
                },
                (value) =>
                {
                    result.IsSuccessful = true;
                    result.EnvironmentList = value;
                },
                (message) =>
                {
                    result.Message = message;
                });

            return result;
        }

        public DetailEnvironmentResult DetailEnvironment(DetailEnvironmentInput model)
        {
            DetailEnvironmentResult result = new DetailEnvironmentResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.GetEnvironment(model.Id);
                },
                (value) =>
                {
                    result.IsSuccessful = true;
                    result.Environment = value;
                },
                (message) =>
                {
                    result.IsSuccessful = false;
                    result.Message = message;
                });

            return result;
        }

        public CreateEnvironmentResult CreateEnvironment(CreateEnvironmentInput model)
        {
            CreateEnvironmentResult result = new CreateEnvironmentResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.CreateEnvironment(model.ApplicationId, model.Name);
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

        public EditEnvironmentResult EditEnvironment(EditEnvironmentInput model)
        {
            EditEnvironmentResult result = new EditEnvironmentResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.EditEnvironment(model.Id, model.ApplicationId, model.Name);
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

        public DeleteEnvironmentResult DeleteEnvironment(DeleteEnvironmentInput model)
        {
            DeleteEnvironmentResult result = new DeleteEnvironmentResult();

            TryGetResult(
                () =>
                {
                    return _dataManager.DeleteEnvironment(model.Id);
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