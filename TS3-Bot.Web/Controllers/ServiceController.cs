namespace DirkSarodnick.TS3_Bot.Web.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Core.Helper;
    using Core.Service;

    /// <summary>
    /// Defines the ServiceController class.
    /// </summary>
    public class ServiceController : Controller
    {
        public JsonResult Index(string id)
        {
            using (var service = new SettingsDirectoryService(BasicHelper.ConfigurationDirectory))
            {
                return Json(service.GetFiles(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Setting(string id)
        {
            using (var service = new SettingsDirectoryService(BasicHelper.ConfigurationDirectory).GetService(id))
            {
                return Json(service.Get(), JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Setting([Required] string id, FormCollection formCollection)
        {
            using (var service = new SettingsDirectoryService(BasicHelper.ConfigurationDirectory).GetService(id))
            {
                var settings = service.Get();
                TryUpdateModel(settings, formCollection);
                service.Set(settings);
                return Json(settings);
            }
        }
    }
}