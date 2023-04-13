using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebAppApiConsuming.Models;

namespace WebAppApiConsuming.Controllers
{
    public class LoginController : Controller
    {
        public  IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://localhost:5214/api/Auth/Login", stringContent))
                {
                    //To get all roles for the user
                    using (var res = await httpClient.PostAsync("http://localhost:5214/api/Auth/", stringContent))
                    {
                        string strRoles = await res.Content.ReadAsStringAsync();
                        var roles = JsonConvert.DeserializeObject<List<string>>(strRoles);
                        TempData["Roles"] = roles;
                    }

                    string token = await response.Content.ReadAsStringAsync();
                    if (token == "Invalid  Username or password")
                    {
                        ViewBag.Message = "Invalid  Username or password";
                        return Redirect(nameof(Index));
                    }
                    HttpContext.Session.SetString("Jwt", token);
                }

                return Redirect("Region/Index");//if token is validated we should be redirected to Region Controller's index action method to show all region data
            }
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }


    }
}
