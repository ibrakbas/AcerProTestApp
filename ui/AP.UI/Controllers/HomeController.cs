using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AP.UI.Models;
using AP.UI.Services;
using AP.Data.dtos;
using AP.Data.dtos.responses;
using AP.Data.entites;

namespace AP.UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientService httpClientService;

    public HomeController(ILogger<HomeController> logger, IHttpClientService service)
    {
        _logger = logger;
        httpClientService = service;
    }

    public IActionResult Department()
    {
        return View();
    }
    public async Task<IActionResult> CreateWorker()
    {
        var response = await httpClientService.GetAsync<List<Departments>>("department/getall");
        ViewBag.Departments = response;
        return View();
    }
    public async  Task<IActionResult> AddWorker(WorkerDto dto)
    {
        try
        { 
            var response = await httpClientService.PostAsync<WorkerDto, Workers>("worker/add", dto);

            if (response.Id>0)
            {
             
                return RedirectToAction("Worker");
            }
             
            return View();
        }
        catch (Exception ex)
        {
            // Hata durumunda loglama yapýyoruz ve kullanýcýya hata mesajý gösteriyoruz
            _logger.LogError(ex, "Worker Ekleme esnasýnda hata oluþtu.");
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
    }

    
    public async Task<IActionResult> Worker()
    {
        try
        {
            // API'den çalýþanlarý almak için istek gönderiyoruz
            var response = await httpClientService.GetAsync<List<Workers>>("worker/getall");

            // Eðer istek baþarýlýysa, çalýþanlarý ViewBag'e ekliyoruz 
            ViewBag.Workers = response;
            return View();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "worker getall esnasýnda hata oluþtu.");
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        try
        {
            // Login iþlemi için API'ye istek gönderiyoruz
            var response = await httpClientService.PostAsync<LoginDto, LoginResponse>("login", dto);

            if (response.loginSuccess)
            {
                // Eðer login baþarýlýysa, token'ý alýyoruz ve çalýþanlar sayfasýna yönlendiriyoruz
                string token = response.token;
                HttpContext.Session.SetString("Token", token);
                return RedirectToAction("Worker");
            }

            // Eðer login baþarýsýzsa, hata mesajýný gösteriyoruz
            ViewBag.ErrorMessage = "Kullanýcý Adý veya þifre hatalý";
            return View();
        }
        catch (Exception ex)
        {
            // Hata durumunda loglama yapýyoruz ve kullanýcýya hata mesajý gösteriyoruz
            _logger.LogError(ex, "Login esnasýnda hata oluþtu.");
            ViewBag.ErrorMessage = ex.Message;
            return View();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
