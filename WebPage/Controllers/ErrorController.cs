using System.Web.Mvc;


namespace WebPage.Controllers
{
    [RoutePrefix("error")]
    public class ErrorController : Controller
    {
        #region Telas de Erros

        [Route("httperror400")]
        public ActionResult HttpError400(string message)
        {
            TempData["mensagemErro"] = "HttpError400: " + message;
            return View("httperror400");
        }

        [Route("httperror404")]
        public ActionResult HttpError404(string message)
        {
            TempData["mensagemErro"] = "HttpError404: " + message;
            return View("httperror404");
        }

        [Route("httperror500")]
        public ActionResult HttpError500(string message)
        {
            TempData["mensagemErro"] = "HttpError500: " + message;
            return View("httperror500");
        }

        [Route("general")]
        public ActionResult General(string message)
        {
            TempData["mensagemErro"] = "ErrorGeneral: " + message;
            return View("general");
        }

        [Route("nullreferenceexception")]
        public ActionResult NullReferenceException(string message)
        {
            TempData["mensagemErro"] = "ErrorNullReferenceException: " + message;
            return View("nullreferenceexception");
        }

        #endregion

    }
}
