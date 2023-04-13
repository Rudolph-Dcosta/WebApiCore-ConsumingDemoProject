using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppApiConsuming.Models;
using System.Net.Http.Headers;
using System.Text;

namespace WebAppApiConsuming.Controllers
{
    public class RegionController : Controller
    {
        public static string baseUrl = "http://localhost:5214/api/Regions/";

        
        public async Task<IActionResult> Index()
        {
            var regions = await GetAllRegions();

            ViewBag.Roles = TempData["Roles"];
            TempData.Keep("Roles");
            foreach (var role in ViewBag.Roles)
            {
                if (role == "admin")
                {
                    ViewBag.Role = "admin";
                }
            }

            return View(regions);
        }
        public async Task<List<Region>> GetAllRegions()
        {
            var accessToken = HttpContext.Session.GetString("Jwt");
            var url = baseUrl;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",accessToken);
            string jsonString = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<List<Region>>(jsonString);
            return res.ToList();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,AreaName,Area,Latitude,Longitude,Population")] Region region)
        {
            var accessToken = HttpContext.Session.GetString("Jwt");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var stringContent = new StringContent(JsonConvert.SerializeObject(region), Encoding.UTF8, "application/json");
            await client.PostAsync(baseUrl, stringContent);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var accessToken = HttpContext.Session.GetString("Jwt");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonStr = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<Region>(jsonStr);
            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,AreaName,Area,Latitude,Longitude,Population")] Region regions)
        {
            if (id != regions.Id)
            {
                return NotFound();
            }
            var accessToken = HttpContext.Session.GetString("Jwt");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var stringContent = new StringContent(JsonConvert.SerializeObject(regions), Encoding.UTF8, "application/json");
            await client.PutAsync(url, stringContent);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var accessToken = HttpContext.Session.GetString("Jwt");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonStr = await client.GetStringAsync(url);
            var res = JsonConvert.DeserializeObject<Region>(jsonStr);
            if (res == null)
            {
                return NotFound();
            }
            return View(res);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accessToken = HttpContext.Session.GetString("Jwt");
            var url = baseUrl + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            await client.DeleteAsync(url);
            return RedirectToAction(nameof(Index));
        }
    }
}
