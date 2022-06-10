using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RealEsWebMVC.Models;
using RealEsWebMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using X.PagedList;

namespace RealEsWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private readonly Uri url = new Uri("https://localhost:44363/api/Offer");
        private readonly Uri url2 = new Uri("https://localhost:44363/api/Apartment");
        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {
            var currentClient = new HttpClient();

            currentClient.BaseAddress = url2;
            currentClient.DefaultRequestHeaders.Accept.Clear();
            currentClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = await currentClient.GetAsync("");

            string jsonString2 = await responseMessage.Content.ReadAsStringAsync();
            var currentResponseData = JsonConvert.DeserializeObject<List<ApartmentVM>>(jsonString2);


            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.GetAsync("");

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<OfferVM>>(jsonString);
                currentResponseData.ForEach(i =>
                {
                    responseData.ForEach(k =>
                    {
                        if (k.ApartmentId == i.Id)
                        {
                            k.ApartmentName = i.ApartmentType;
                            k.TownName = i.Town.TownName;
                            k.Img = i.Image;
                            k.Area = i.Area;
                        }
                    });
                });

                if (searchString != null)
                {
                    page = 1;
                }
                else
                {
                    searchString = currentFilter;
                }

                ViewBag.CurrentFilter = searchString;
                if (!String.IsNullOrEmpty(searchString))
                {
                    responseData = responseData.Where(c => c.ApartmentName.Contains(searchString)).ToList();

                }
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                responseData = responseData.ToList();

                return View(responseData.ToPagedList(pageNumber, pageSize));
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
