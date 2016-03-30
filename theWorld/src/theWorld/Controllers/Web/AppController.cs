namespace theWorld.Controllers.Web
{
    using System.Diagnostics.CodeAnalysis;

    using Microsoft.AspNet.Mvc;

    using theWorld.Models;
    using theWorld.Services;
    using theWorld.ViewModels;
    using System.Linq;

    using Microsoft.AspNet.Authorization;

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class AppController : Controller
    {
        private IMailService _mailService;
        private readonly IWorldRepository _repository;

        public AppController(IMailService service, IWorldRepository repository)
        {
            _mailService = service;
            _repository = repository;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            //var trips = this._repository.GetAllTrips();
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];
                if (string.IsNullOrWhiteSpace(email))
                {
                    this.ModelState.AddModelError("","Cound not send email, Configuration problem");
                }
                if (this._mailService.SendMail(
                    email,
                    email,
                    $"Contact Page from {model.Name} ({model.Email})",
                    model.Message))
                {
                    this.ModelState.Clear();
                    ViewBag.Message ="Mail Sent. Thanks!";
                }
            }            
            return View();
        }
    }
}
