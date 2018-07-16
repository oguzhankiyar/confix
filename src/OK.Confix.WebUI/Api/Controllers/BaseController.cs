using System;

namespace OK.Confix.WebUI.Api.Controllers
{
    internal abstract class BaseController
    {
        public void TryGetResult<T>(Func<T> func, Action<T> success, Action<string> error)
        {
            try
            {
                T result = func();

                success(result);
            }
            catch (Exception ex)
            {
                error(ex.Message);
            }
        }
    }
}