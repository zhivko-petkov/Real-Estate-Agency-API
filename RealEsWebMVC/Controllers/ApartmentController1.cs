using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEsWebMVC.Utils;
using RealEsWebMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using X.PagedList;

namespace RealEsWebMVC.Controllers
{
    public class ApartmentController1 : Controller
    {
        private readonly Uri url = new Uri("https://localhost:44363/api/Apartment");
        // GET: ApartmentController1
        public async Task<ActionResult> Index(string searchString, string currentFilter, int? page)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.GetAsync("");

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<ApartmentVM>>(jsonString);

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
                    responseData = responseData.Where(c => c.ApartmentType.Contains(searchString)).ToList();
           
                }
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                responseData = responseData.ToList();

                return View(responseData.ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: ApartmentController1/Details/5
        public async Task<ActionResult> Details(int id)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.GetAsync("Apartment/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ApartmentVM>(jsonString);
                return View(responseData);
            }
        }

        // GET: ApartmentController1/Create
        public ActionResult Create()
        {
            ViewBag.Towns = LoadDataUtil.LoadTownData();
            return View();
        }

        // POST: ApartmentController1/Create
        [HttpPost]
        public async Task<ActionResult> Create(ApartmentVM apartmentVM)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                   /* apartmentVM.Town = new TownDTO();
                    apartmentVM.Town.Id = apartmentVM.TownId;*/

                    var content = JsonConvert.SerializeObject(apartmentVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request
                    HttpResponseMessage response = await client.PostAsync("", byteContent);

                    // for testing purposes
                    // string jsonString = await response.Content.ReadAsStringAsync();

                }
                
                return RedirectToAction("Index");
            }
            catch
            { 
               //ViewBag.Towns = LoadDataUtil.LoadTownData();
                return View();
            }
        }

        // GET: ApartmentController1/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.GetAsync("apartment/" + id);

                // parse the response and return data
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<ApartmentVM>(jsonString);
                return View(responseData);
            }
        }

        // POST: ApartmentController1/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ApartmentVM apartmentVM)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = JsonConvert.SerializeObject(apartmentVM); 
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request
                    HttpResponseMessage response = await client.PostAsync("", byteContent);

                    // for testing purposes
                    // string jsonString = await response.Content.ReadAsStringAsync();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // POST: ApartmentController1/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            // string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // make the request
                HttpResponseMessage response = await client.DeleteAsync("apartment/" + id);

                // for testing purposes
                // string jsonString = await response.Content.ReadAsStringAsync();
                // var responseData = JsonConvert.DeserializeObject<NationalityViewModel>(jsonString);
                // return View(responseData);
                return RedirectToAction("Index");
            }
        }
    }
}
