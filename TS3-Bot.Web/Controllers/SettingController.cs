namespace DirkSarodnick.TS3_Bot.Web.Controllers
{
    using System.Web.Mvc;
    using Core.Helper;
    using Core.Service;

    /// <summary>
    /// Defines the SettingController class.
    /// </summary>
    public class SettingController : Controller
    {
        public ActionResult Index()
        {
            using (var service = new SettingsDirectoryService(BasicHelper.ConfigurationDirectory))
            {
                return View(service.GetFiles());
            }
        }

        public ActionResult Edit(string id)
        {
            using (var service = new SettingsDirectoryService(BasicHelper.ConfigurationDirectory).GetService(id))
            {
                return View(service.Get());
            }
        }

        [HttpPost]
        public ActionResult Edit(string id, FormCollection formCollection)
        {
            using (var service = new SettingsDirectoryService(BasicHelper.ConfigurationDirectory).GetService(id))
            {
                var settings = service.Get();
                TryUpdateModel(settings, formCollection);
                service.Set(settings);

                return RedirectToAction("Edit", new { id });
            }
        }
    }
}