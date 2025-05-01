using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AP.UI.Models;
using AP.UI.Services;
using AP.Data.dtos;
using AP.Data.dtos.responses;

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

    public IActionResult Worker()
    {
        return View();
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
                return RedirectToAction("Worker");
            }

            // Eðer login baþarýsýzsa, hata mesajýný gösteriyoruz
            ViewBag.ErrorMessage = "Invalid username or password.";
            return View();
        }
        catch (Exception ex)
        {
            // Hata durumunda loglama yapýyoruz ve kullanýcýya hata mesajý gösteriyoruz
            _logger.LogError(ex, "An error occurred during login.");
            ViewBag.ErrorMessage = "An error occurred. Please try again later.";
            return View();
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
