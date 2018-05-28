using System.Threading.Tasks;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Model;

namespace Web.Controllers
{
    public class ConfigurationController : Controller
    {
        private IConfigurationRepository _configurationRepository;

        public ConfigurationController(IConfigurationRepository configurationRepository, IConfiguration configuration)
        {
            this._configurationRepository = configurationRepository;
            _configurationRepository.ConnectionString = configuration.GetConnectionString("ConfDb");
        }

        public async Task<IActionResult> Index()
        {
            var list = await _configurationRepository.GetAllAsync();

            return View(list);
        }


        public IActionResult Error()
        {
            return NoContent();
        }

        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Insert(ConfigurationObject configurationObject)
        {
            await _configurationRepository.InsertAsync(configurationObject);

            return RedirectToAction("Success");
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Update(ConfigurationObject configurationObject)
        {
            await _configurationRepository.UpdateAsync(configurationObject);

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var model = _configurationRepository.FindById(id);

            return View(model);
        }
    }
}